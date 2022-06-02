using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroGenerarCodigoDTO
    {
        public int IdProgramaGeneral { get; set; }
        public string Modalidad { get; set; }
        public int? IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public int Anio { get; set; }
    }
}
