using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePendienteFiltroDTO
    {
        public DateTime PeriodoInicio { get; set; }
        public DateTime PeriodoFin { get; set; }
        public string PeriodoCierre { get; set; }
        public List<int> Modalidad { get; set; }
        public List<string> Coordinadora { get; set; }
        public string EstadoPersonal { get; set; }
    }

    public class ReportePendientePeriodoFiltroDTO
    {
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaCorte { get; set; }
        public DateTime FechaCortePrevio { get; set; }
        public List<int> Modalidad { get; set; }
        public List<string> Coordinadora { get; set; }
    }
    public class ReportePendienteMesCoordinadorFiltroDTO
    {
        public int PeriodoInicial { get; set; }
        public int PeriodoFin { get; set; }
        public DateTime FechaCorte1 { get; set; }
        public DateTime FechaCorte2 { get; set; }
        public List<int> Modalidad { get; set; }
        public List<string> Coordinadora { get; set; }
        public DateTime FechaPagoInicial { get; set; }
        public DateTime FechaPagoFinal { get; set; }
    }
}
