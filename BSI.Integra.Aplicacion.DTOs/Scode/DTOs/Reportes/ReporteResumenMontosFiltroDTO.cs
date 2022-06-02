using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteResumenMontosFiltroDTO
    {
        public int PeriodoActual { get; set; }
        public int? PeriodoCierre { get; set; }      
    }
    public class ReporteResumenMontosFiltroGeneralDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int? PeriodoActual { get; set; }
    }
}
