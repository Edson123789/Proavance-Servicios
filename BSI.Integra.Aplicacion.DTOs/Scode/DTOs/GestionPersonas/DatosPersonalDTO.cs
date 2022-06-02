using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosPersonalDTO
    {
        public PersonalDTO Personal { get; set; }
        public PersonalCeseDTO Cese { get; set; }
        public string Usuario { get; set; }
    }
}
