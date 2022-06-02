using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteCambiosDTO
    {
        public int IdCronogramaMod { get; set; }
        public int IdAlumno { get; set; }
        public string Modalidad { get; set; }
        public DateTime FechaCambio { get; set; }
        public string Ciudad { get; set; }
        public string Programa { get; set; }
        public string Alumno { get; set; }
        public int IdCentroCosto { get; set; }
        public string CodigoAlumno { get; set; }
        public int IdMatricula { get; set; }
        public string Observaciones { get; set; }
        public string RealizadoPor { get; set; }
        public string MensajeSistema { get; set; }
        public string SolicitadoPor { get; set; }
        public string AprobadoPor { get; set; }
        public string Observaciones2 { get; set; }

    }

}