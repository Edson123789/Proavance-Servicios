using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class CronogramaPagoDetalleFinalPorPeriodoBO : BaseBO
    {

        public int IdPeriodo { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Mora { get; set; }
        public decimal? MontoPagado { get; set; }
        public bool? Cancelado { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaPagoBanco { get; set; }
        public string MonedaPago { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public int? Version { get; set; }
        public bool? Aprobado { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime? FechaProcesoPagoReal { get; set; }
        public DateTime? FechaIngresoEnCuenta { get; set; }
        public DateTime? FechaEfectivoDisponible { get; set; }
    }
}
