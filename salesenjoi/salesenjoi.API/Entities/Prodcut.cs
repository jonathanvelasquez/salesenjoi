using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace salesenjoi.API.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caractéres")]
        public string Name { get; set; } = null;

        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = null;

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [DisplayName("¿Está activo?")]
        public bool IsActive { get; set; }

        public Category Category { get; set; } = null;
    }
}
