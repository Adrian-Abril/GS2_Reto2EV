<?php
/**
 * Clase Conexion - Maneja la conexión a la base de datos MySQL
 * 
 * Esta clase implementa el patrón Singleton para asegurar
 * que solo exista una instancia de la conexión en toda la aplicación
 */
class Conexion {
    // Propiedades de la clase
    private static $instancia = null;
    private $conexion;
    private $host = 'localhost';
    private $usuario = 'root';
    private $password = '';
    private $base_datos = 'farmed';
    
    /**
     * Constructor privado para prevenir la creación directa del objeto
     */
    private function __construct() {
        // Configuración de encabezados CORS
        header("Access-Control-Allow-Origin: *");
        header("Access-Control-Allow-Methods: GET, POST, PUT, DELETE, OPTIONS");
        header("Access-Control-Allow-Headers: Content-Type, Authorization, X-Requested-With");
        header("Content-Type: application/json; charset=UTF-8");
        
        // Manejo de solicitudes OPTIONS (preflight)
        if ($_SERVER['REQUEST_METHOD'] == 'OPTIONS') {
            header("HTTP/1.1 200 OK");
            exit;
        }
        
        // Establecer la conexión
        $this->conectar();
    }
    
    /**
     * Método para obtener la instancia única de la clase (Singleton)
     * @return Conexion
     */
    public static function obtenerInstancia() {
        if (self::$instancia === null) {
            self::$instancia = new self();
        }
        return self::$instancia;
    }
    
    /**
     * Método privado para establecer la conexión a la base de datos
     */
    private function conectar() {
        try {
            $this->conexion = new mysqli(
                $this->host,
                $this->usuario,
                $this->password,
                $this->base_datos
            );
            
            // Verificar si hubo error en la conexión
            if ($this->conexion->connect_error) {
                throw new Exception("Error de conexión: " . $this->conexion->connect_error);
            }
            
            // Configurar charset
            $this->conexion->set_charset("utf8");
            
        } catch (Exception $e) {
            $this->responderError($e->getMessage());
            die();
        }
    }
    
    /**
     * Obtiene la conexión a la base de datos
     * @return mysqli
     */
    public function getConexion() {
        return $this->conexion;
    }
        /**
     * SOBRA  
     * */
    /**
     * Ejecuta una consulta SQL
     * @param string $sql Consulta SQL a ejecutar
     * @return mysqli_result|bool
     */
    public function ejecutarConsulta($sql) {
        try {
            $resultado = $this->conexion->query($sql);
            if ($resultado === false) {
                throw new Exception("Error en la consulta: " . $this->conexion->error);
            }
            return $resultado;
        } catch (Exception $e) {
            $this->responderError($e->getMessage());
            return false;
        }
    }
    
    /**
     * Ejecuta una consulta preparada
     * @param string $sql Consulta SQL con placeholders
     * @param string $tipos Tipos de datos para los parámetros (s:string, i:integer, d:double, b:blob)
     * @param array $params Parámetros para la consulta
     * @return mysqli_stmt|bool
     */
    public function ejecutarPreparada($sql, $tipos, $params) {
        try {
            $stmt = $this->conexion->prepare($sql);
            
            if ($stmt === false) {
                throw new Exception("Error preparando consulta: " . $this->conexion->error);
            }
            
            if (!empty($params)) {
                $stmt->bind_param($tipos, ...$params);
            }
            
            $stmt->execute();
            
            return $stmt;
        } catch (Exception $e) {
            $this->responderError($e->getMessage());
            return false;
        }
    }
    
    /**
     * Devuelve el ID del último registro insertado
     * @return int
     */
    public function ultimoIdInsertado() {
        return $this->conexion->insert_id;
    }
    
    /**
     * Escapa caracteres especiales en una cadena para usarla en una consulta SQL
     * @param string $cadena
     * @return string
     */
    public function escapar($cadena) {
        return $this->conexion->real_escape_string($cadena);
    }
    
    /**
     * Cierra la conexión a la base de datos
     */
    public function cerrarConexion() {
        if ($this->conexion) {
            $this->conexion->close();
        }
    }
    
    /**
     * Envía una respuesta de error en formato JSON
     * @param string $mensaje Mensaje de error
     */
    private function responderError($mensaje) {
        $respuesta = array(
            "estado" => "error",
            "mensaje" => $mensaje
        );
        echo json_encode($respuesta);
    }
    
    /**
     * Convierte un resultado de consulta a un array asociativo
     * @param mysqli_result $resultado
     * @return array
     */
    public function resultadoToArray($resultado) {
        $datos = array();
        while ($fila = $resultado->fetch_assoc()) {
            $datos[] = $fila;
        }
        return $datos;
    }
    
    /**
     * Método para prevenir la clonación del objeto
     */
    private function __clone() {}
    
    /**
     * Método para prevenir la deserialización del objeto
     */
    public function __wakeup() {}
}
?>