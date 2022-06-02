using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class CronogramaPagoDetalleFinalDTO
    {
        public int Id { get; set; }
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
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? IdFormaPago { get; set; }
        public string NombreFormaPago { get; set; }
        public int? IdCuenta { get; set; }
        public DateTime? FechaPagoBanco { get; set; }
        public bool? Enviado { get; set; }
        public string Observaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        public string NroDocumento { get; set; }
        public string MonedaPago { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal? CuotaDolares { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public int? Version { get; set; }
        public bool? Aprobado { get; set; }
        public DateTime? FechaDeposito { get; set; }
        public int? WebMoneda { get; set; }
        public double? WebTipoCambio { get; set; }
        public decimal? MoraTarifario { get; set; }
        public DateTime? FechaCompromiso { get; set; }
        public int? VersionCompromiso { get; set; }
        public decimal? MontoCompromiso { get; set; }
        public DateTime? FechaGeneradoCompromiso { get; set; }
    }
}
