namespace FarMedAPI.Models
{
    public class Entrega
    {
        public int Id_Entrega { get; set; }
        public int Id_Pedido { get; set; }
        public DateTime Fecha_Entrega { get; set; }
        public string Estado { get; set; } // En proceso, Entregado, Cancelado
        public int Id_Empleado { get; set; }
    }
}
