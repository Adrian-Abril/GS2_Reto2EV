using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarMedAPI.Models
{
    public class Producto
    {
        [Key]
        public int Id_Producto { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
        
        [StringLength(500)]
        public string Descripcion { get; set; }
        
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Precio { get; set; }
        
        [Required]
        public int Stock { get; set; }
        
        public bool Disponible { get; set; }
        
        [Required]
        public DateTime Fecha_Creacion { get; set; }
        
        // Relaciones
        [Required]
        public int Id_Categoria { get; set; }
        
        [ForeignKey("Id_Categoria")]
        public virtual Categoria Categoria { get; set; }
        
        public int? Id_Laboratorio { get; set; }
        
        [ForeignKey("Id_Laboratorio")]
        public virtual Laboratorio Laboratorio { get; set; }
    }
}