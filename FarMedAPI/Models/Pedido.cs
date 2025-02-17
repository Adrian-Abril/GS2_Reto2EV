using System;

namespace FarMedAPI.Models
{
    public class Pedido
    {
        public int Id_Pedido { get; set; }
        public int Id_Cliente { get; set; }
        public int Id_Farmacia { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public string Estado { get; set; } // Pendiente, Enviado, Entregado
        public decimal Total { get; set; }
        public string Dirección_Entrega { get; set; }
    }
}
