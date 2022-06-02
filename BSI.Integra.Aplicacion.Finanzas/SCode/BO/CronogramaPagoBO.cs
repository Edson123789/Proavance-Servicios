using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CronogramaPagoBO : BaseBO
    {
        public int? IdMatriculaCabecera { get; set; }
        public int? IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public string Periodo { get; set; }
        public string Moneda { get; set; }
        public string AcuerdoPago { get; set; }
        public double? TipoCambio { get; set; }
        public double? TotalPagar { get; set; }
        public int? NroCuotas { get; set; }
        public DateTime? FechaIniPago { get; set; }
        public bool? Enviado { get; set; }
        public string Observaciones { get; set; }
        public bool? ConCuotaInicial { get; set; }
        public decimal? CuotaInicial { get; set; }
        public bool? CadaNdias { get; set; }
        public int? Ndias { get; set; }
        public string WebMoneda { get; set; }
        public double? WebTipoCambio { get; set; }
        public double? WebTotalPagar { get; set; }
        public double? WebTotalPagarConv { get; set; }
        public string IdMigracion { get; set; }
    }
}
