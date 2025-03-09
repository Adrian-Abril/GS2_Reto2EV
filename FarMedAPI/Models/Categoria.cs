// Models/Categoria.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FarMedAPI.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        
        [StringLength(255)]
        public string? Descripcion { get; set; }
        
        // Propiedades de navegación
        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}