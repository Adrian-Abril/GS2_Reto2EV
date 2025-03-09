using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FarMedAPI.Models
{
    public class Laboratorio
    {
        [Key]
        public int LaboratorioId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        
        [StringLength(255)]
        public string? Direccion { get; set; }
        
        [StringLength(20)]
        public string? Telefono { get; set; }
        
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        
        // Propiedades de navegación
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}