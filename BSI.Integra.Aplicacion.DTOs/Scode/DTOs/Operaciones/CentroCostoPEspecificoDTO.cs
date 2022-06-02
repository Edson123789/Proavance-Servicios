using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CentroCostoPEspecificoDTO
    {
        public int IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
    }
}
