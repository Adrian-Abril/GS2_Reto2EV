namespace FarMedAPI.Models
{
    public class DetallePedido
    {
        public int Id_Detalle { get; set; }
        public int Id_Pedido { get; set; }
        public int Id_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
    }
}
