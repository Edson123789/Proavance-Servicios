using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TotalRN2OportunidadDTO
    {
        public int TotalRN2AProcesar { get; set; }
        public int TotalRN2Clasificados { get; set; }
        public string ExisteError { get; set; }
        public string Error{ get; set; }
    }
}
