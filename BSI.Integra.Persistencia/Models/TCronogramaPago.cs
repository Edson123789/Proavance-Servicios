using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaPago
    {
        public int Id { get; set; }
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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public string IdMigracion { get; set; }
    }
}
