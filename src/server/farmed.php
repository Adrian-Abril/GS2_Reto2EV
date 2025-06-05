<?php
include_once "./../db/conection.php";
$objeto = new Conexion();
$conexion = $objeto->getConexion();

// Procesar input de JSON
$_POST = json_decode(file_get_contents("php://input"), true);
// peticion usuario
$option = $_POST['opcion'];
?>