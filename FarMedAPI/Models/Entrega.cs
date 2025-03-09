// Models/Entrega.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarMedAPI.Models
{
    public class Entrega
    {
        [Key]
        public int EntregaId { get; set; }
        
        public int PedidoId { get; set; }
        
        public DateTime? FechaEntrega { get; set; }
        
        [Required]
        [StringLength(255)]
        public string DireccionEntrega { get; set; } = null!;
        
        [Required]
        [StringLength(50)]
        public string EstadoEntrega { get; set; } = null!;
        
        [Required]
        [StringLength(50)]
        public string MetodoEntrega { get; set; } = null!;
        
        // Propiedades de navegación
        [ForeignKey("PedidoId")]
        public virtual Pedido? Pedido { get; set; }
    }
}