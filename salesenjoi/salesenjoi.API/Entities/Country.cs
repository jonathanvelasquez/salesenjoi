using System.ComponentModel.DataAnnotations;

namespace salesenjoi.API.Entities
{
    public class Country
    {
        public int id { get; set; }

        [Display(Name = "Pais")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe de tener maximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} debe de ser obligatorio")]
        public string? Name { get; set; } = null;
    }
}
