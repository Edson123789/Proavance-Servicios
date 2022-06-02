using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCodigosDTO
    {
        public int IdAlumno { get; set; }
        public string Modalidad { get; set; }
        public string Ciudad { get; set; }
        public string Programa { get; set; }
        public int? IdCentroCosto { get; set; }
        public string Codigo { get; set; }
        public int IdMatricula { get; set; }
        public string Alumno { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

}