using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class GoogleAnalyticsReporteDetalleBO : BaseBO
    {
        public int IdGoogleAnalyticsReportePagina { get; set; }
        public int IdGoogleAnalyticsSegmento { get; set; }
        public int IdGoogleAnalyticsMetrica { get; set; }
        public int Mes { get; set; }
        public int Anho { get; set; }
        public string Valor { get; set; }
        public int? IdMigracion { get; set; }
    }
}
