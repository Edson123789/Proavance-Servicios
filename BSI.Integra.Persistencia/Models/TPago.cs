using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPago
    {
        public int Id { get; set; }
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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
