using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Dependiente
    {
        public int IdDependiente { get; set; }
        public string IdEmpleado { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Genero { get; set; }
        public string? Telefono { get; set; }
        public string? Rfc { get; set; }
        public byte IdDependienteTipo { get; set; }

        public virtual DependienteTipo IdDependienteTipoNavigation { get; set; } = null!;
        public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

        //Alias

        public string? NombreEmpleado { get; set; }
        public string? ApellidoPaternoEmpleado { get; set; }
        public string? ApellidoMaternoEmpleado { get; set; }

        public string? NombreTipo { get; set; }
    }
}
