using Google.Apis.AnalyticsReporting.v4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.DTOs.Reportes
{
    public class ReporteAnalyticsPorMesDTO
    {
        public int Mes { get; set; }
        public int Anho { get; set; }
        public Report Reporte { get; set; }

    }
}
