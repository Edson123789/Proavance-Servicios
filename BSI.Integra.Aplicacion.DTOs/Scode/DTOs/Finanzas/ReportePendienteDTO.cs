using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReportePendienteDTO
    {
        public string PeriodoVencimiento { get; set; }
        public string Coordinador { get; set; }
        public int Proyectado { get; set; }
        public int Actual { get; set; }
        public int DiferenciaActualOriginal { get; set; }   
        public int DiferenciaRetirosConDevolucion { get; set; }
        public int DiferenciaRetirosSinDevolucion { get; set; }
        public int MontoPagadoProyectado { get; set; }
        public int MontoPagadoTotal { get; set; }
        public int MontoPagadoVentas { get; set; }
        public int MontoRecuperadoMes { get; set; }
        public int ProyectadoVentas { get; set; }
        public int ModificacionesSinRetiro { get; set; }
    }
    public class ReportePendienteAgrupadoDTO
    {
        public string PeriodoVencimiento { get; set; }
        public ReportePendienteAgrupadoDetallePeriodoDTO DetallePeriodo { get; set; }
    }
    public class ReportePendienteAgrupadoDetallePeriodoDTO {

        public string Coordinador { get; set; }
        public int Proyectado { get; set; }
        public int Actual { get; set; }
        public int DiferenciaActualOriginal { get; set; }
        public int DiferenciaRetirosConDevolucion { get; set; }
        public int DiferenciaRetirosSinDevolucion { get; set; }
        public int MontoPagadoProyectado { get; set; }
        public int MontoPagadoTotal { get; set; }
        public int MontoPagadoVentas { get; set; }
        public int MontoRecuperadoMes { get; set; }
        public int ProyectadoVentas { get; set; }
        public int ModificacionesSinRetiro { get; set; }
    }
}
