using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class FiltroReporteMarcacionDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<int> Personal { get; set; }
        public List<int> Sede { get; set; }
        public List<int> Area { get; set; }

    }

    public class AgrupadoReporteMarcacionDTO
    {
        public string Fecha { get; set; }
        public string Sede { get; set; }
        public string AreaTrabajo { get; set; }
        public string Nombres { get; set; }
        public string Jefe { get; set; }
        public string TipoPersonal { get; set; }
        public TimeSpan? M1 { get; set; }
        public TimeSpan? M2 { get; set; }
        public TimeSpan? M3 { get; set; }
        public TimeSpan? M4 { get; set; }
        public TimeSpan? H1 { get; set; }
        public TimeSpan? H2 { get; set; }
        public TimeSpan? H3 { get; set; }
        public TimeSpan? H4 { get; set; }
        public int Total { get; set; }
    }
}
