using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FarMedAPI.Models
{
    public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
