// Models/Producto.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarMedAPI.Models
{
    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        
        [StringLength(255)]
        public string? Descripcion { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }
        
        [Required]
        public int Stock { get; set; }
        
        public int? CategoriaId { get; set; }
        
        public int? LaboratorioId { get; set; }
        
        public bool RequiereReceta { get; set; }
        
        // Propiedades de navegación
        [ForeignKey("CategoriaId")]
        public virtual Categoria? Categoria { get; set; }
        
        [ForeignKey("LaboratorioId")]
        public virtual Laboratorio? Laboratorio { get; set; }
        
        public virtual ICollection<DetallePedido> DetallesPedidos { get; set; } = new List<DetallePedido>();
    }
}