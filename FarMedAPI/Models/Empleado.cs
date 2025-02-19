namespace FarMedAPI.Models
{
    public class Empleado
    {
        public int Id_Empleado { get; set; }
        public int Id_Farmacia { get; set; }
        public string Nombre { get; set; }
        public string Teléfono { get; set; }
        public string Rol { get; set; } // Farmacéutico, Repartidor
    }
}
