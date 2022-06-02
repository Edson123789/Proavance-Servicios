using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TGoogleAnalyticsReporteDetalle
    {
        public int Id { get; set; }
        public int IdGoogleAnalyticsReportePagina { get; set; }
        public int IdGoogleAnalyticsSegmento { get; set; }
        public int IdGoogleAnalyticsMetrica { get; set; }
        public int Mes { get; set; }
        public int Anho { get; set; }
        public string Valor { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
