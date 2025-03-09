using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarMedAPI.Models
{
    public class DetallePedido
    {
        [Key, Column(Order = 0)]
        public int PedidoId { get; set; }
        
        [Key, Column(Order = 1)]
        public int ProductoId { get; set; }
        
        [Required]
        public int Cantidad { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PrecioUnitario { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Subtotal { get; set; }
        
        // Propiedades de navegación
        [ForeignKey("PedidoId")]
        public virtual Pedido? Pedido { get; set; }
        
        [ForeignKey("ProductoId")]
        public virtual Producto? Producto { get; set; }
    }
}