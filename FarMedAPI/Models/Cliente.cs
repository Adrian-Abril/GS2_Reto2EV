// Models/Cliente.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FarMedAPI.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        
        [Required]
        [StringLength(100)]
        public string Apellidos { get; set; } = null!;
        
        [StringLength(20)]
        public string? DNI { get; set; }
        
        [StringLength(255)]
        public string? Direccion { get; set; }
        
        [StringLength(20)]
        public string? Telefono { get; set; }
        
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        
        public DateTime? FechaNacimiento { get; set; }
        
        // Propiedades de navegación
        public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}