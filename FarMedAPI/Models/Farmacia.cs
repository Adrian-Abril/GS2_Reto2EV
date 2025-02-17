namespace FarMedAPI.Models
{
    public class Farmacia
    {
        public int Id_Farmacia { get; set; }
        public string Nombre { get; set; }
        public string Dirección { get; set; }
        public string Teléfono { get; set; }
        public string Horario_Apertura { get; set; }
        public string Horario_Cierre { get; set; }
    }
}
