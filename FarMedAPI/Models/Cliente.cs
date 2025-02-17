using System;

namespace FarMedAPI.Models
{
    public class Cliente
    {
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Teléfono { get; set; }
        public string Tipo_Usuario { get; set; } // Cliente, Farmacéutico, Repartidor
        public string Dirección { get; set; }
        public DateTime Fecha_Registro { get; set; }

        public Cliente()
        {
            Fecha_Registro = DateTime.UtcNow;
        }
    }
}
