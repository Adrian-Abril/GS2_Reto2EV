namespace FarMedAPI.Models
{
    public class Producto
    {
        public int Id_Producto { get; set; }
        public int Id_Farmacia { get; set; }
        public string Nombre { get; set; }
        public string Descripción { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
    