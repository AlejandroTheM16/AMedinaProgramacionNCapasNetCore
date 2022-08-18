using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion { get; set; }
        public string Nombre { get; set; }
        public string Calle { get; set; }

        [Range(1, 1000,
        ErrorMessage = "Por favor ingrese un numero interior valido.")]
        public string NumeroInterior { get; set; }

        [Range(1, 1000,
        ErrorMessage = "Por favor ingrese un numero exterior valido")]
        public string NumeroExterior { get; set; }
        public ML.Colonia Colonia { get; set; }
        public ML.Usuario Usuario { get; set; }
    }
}
