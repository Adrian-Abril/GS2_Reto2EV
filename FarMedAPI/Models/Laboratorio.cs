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

        public string? Direccion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
