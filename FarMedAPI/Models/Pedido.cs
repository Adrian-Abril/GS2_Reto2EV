using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FarMedAPI.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime FechaPedido { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal Total { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
        public virtual ICollection<DetallePedido> Detalles { get; set; } = new List<DetallePedido>();
    }
}
