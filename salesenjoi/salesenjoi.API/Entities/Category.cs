using System.ComponentModel.DataAnnotations;

namespace salesenjoi.API.Entities
{
    public class Category
    {
        public int id { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Name { get; set; } = null;
    }
}
