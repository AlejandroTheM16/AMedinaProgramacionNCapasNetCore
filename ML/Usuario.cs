using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }

        [Required]
        public string? Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido paterno")]
        public string? ApellidoPaterno { get; set; }

        [Display(Name = "Apellido materno")]
        public string? ApellidoMaterno { get; set; }

        [MaxLength(length: 1)]
        public string? Genero { get; set; }

        public string? Correo { get; set; }

        [MinLength(length: 10, ErrorMessage = "Ingresa un telefono de al menos 10 digitos")]
        [MaxLength(length: 14)]
        public string? Telefono { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        public string? FechaNacimiento { get; set; }

        [MinLength(length: 1)]
        [MaxLength(length: 10)]
        public string? UserName { get; set; }

        [MinLength(length: 1, ErrorMessage = "La contraseña debe ser al menos de un caracter")]
        [MaxLength(length: 10, ErrorMessage = "La contraseña no debe exceder los 10 caracteres")]
        public string? Password { get; set; }

        [StringLength(18,
            ErrorMessage = "El CURP solo esta conformado por 18 digitos")]
        public string? CURP { get; set; }
        public string? Imagen { get; set; }
        public bool Status { get; set; }
        public ML.Rol Rol { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Direccion Direccion { get; set; }

    }
}
