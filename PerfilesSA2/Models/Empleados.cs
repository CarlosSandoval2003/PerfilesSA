using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace PerfilesSA2.Models
{
    public class Empleado
    {
        public int EmpleadoID { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "Los nombres solo pueden contener letras y espacios.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El DPI es obligatorio.")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "El DPI debe contener exactamente 13 dígitos numéricos.")]
        public string DPI { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9À-ÿ\s,-]+$", ErrorMessage = "La dirección solo puede contener letras, números, comas, guiones y espacios.")]
        public string Direccion { get; set; }


        [RegularExpression(@"^\d+$", ErrorMessage = "El NIT solo puede contener dígitos numéricos.")]
        public string NIT { get; set; }

        public int DepartamentoID { get; set; }

        public int Edad => (int)((DateTime.Now - FechaNacimiento).TotalDays / 365.25);
        public int TiempoLaborado => (int)((DateTime.Now - FechaIngreso).TotalDays / 365.25);

        public bool Estado { get; set; }
    }
}
