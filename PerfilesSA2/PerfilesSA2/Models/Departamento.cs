using System.ComponentModel.DataAnnotations;

namespace PerfilesSA2.Models
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "Los nombres solo pueden contener letras y espacios.")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public bool Estado { get; set; } // Campo para habilitar/deshabilitar
    }
}