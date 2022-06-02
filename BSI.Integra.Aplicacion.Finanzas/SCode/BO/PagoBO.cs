using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class PagoBO:BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public double? TipoCambio { get; set; }
        public string Concepto { get; set; }
        public string Ruc { get; set; }
        public int? IdFormaPago { get; set; }
        public string SerieNumero { get; set; }
        public int? IdCuentaCorriente { get; set; }
        public string NroRefCheque { get; set; }
        public DateTime? FechaDocumento { get; set; }
        public string NroDeposito { get; set; }
        public DateTime? FechaPago { get; set; }
        public int? IdDocumentoPago { get; set; }
        public int? IdMigracion { get; set; }
    }
}
