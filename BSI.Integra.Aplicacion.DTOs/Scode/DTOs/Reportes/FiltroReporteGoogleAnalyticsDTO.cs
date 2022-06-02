using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FiltroReporteGoogleAnalyticsDTO
    {
        public int[] Segmentos { get; set; }
        public FiltroMetricasGoogleAnalyticsDTO[] Metricas { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int IdReporteAnalyticsFiltro { get; set; }
        public bool Mensual { get; set; }
        public string Usuario { get; set; }

    }
}
