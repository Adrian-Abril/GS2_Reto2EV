// Models/Pedido.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarMedAPI.Models
{
    public class Pedido
    {
        [Key]
        public int PedidoId { get; set; }
        
        public int? ClienteId { get; set; }
        
        [Required]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        
        [Required]
        [StringLength(50)]
        public string EstadoPedido { get; set; } = null!;
        
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalPedido { get; set; }
        
        public int? FarmaciaId { get; set; }
        
        public int? EmpleadoId { get; set; }
        
        // Propiedades de navegación
        [ForeignKey("ClienteId")]
        public virtual Cliente? Cliente { get; set; }
        
        [ForeignKey("FarmaciaId")]
        public virtual Farmacia? Farmacia { get; set; }
        
        [ForeignKey("EmpleadoId")]
        public virtual Empleado? Empleado { get; set; }
        
        public virtual ICollection<DetallePedido> DetallesPedidos { get; set; } = new List<DetallePedido>();
        
        public virtual Entrega? Entrega { get; set; }
    }
}