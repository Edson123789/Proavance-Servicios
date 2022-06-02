using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaConversionConsolidadaGraficaFiltroDTO
    {
        
        public List<int> Coordinadores { get; set; }
        public List<int> Asesores { get; set; }
        public string PeriodoInicio { get; set; }
        public string PeriodoFin { get; set; }
        public int? TipoPeriodo { get; set; }
       
    }
}
