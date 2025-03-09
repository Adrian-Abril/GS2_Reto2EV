// Models/Empleado.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarMedAPI.Models
{
    public class Empleado
    {
        [Key]
        public int EmpleadoId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        
        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = null!;
        
        [StringLength(20)]
        public string? DNI { get; set; }
        
        [Required]
        public DateTime FechaContratacion { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Cargo { get; set; } = null!;
        
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Salario { get; set; }
        
        public int? FarmaciaId { get; set; }
        
        // Propiedades de navegación
        [ForeignKey("FarmaciaId")]
        public virtual Farmacia? Farmacia { get; set; }
        
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}