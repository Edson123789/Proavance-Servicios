using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaPagoDetalleFinalPorDia
    {
        public int Id { get; set; }
        public DateTime FechaCierre { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int IdPespecifico { get; set; }
        public string EstadoMatricula { get; set; }
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
        public DateTime? FechaMatricula { get; set; }
        public string MonedaPago { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public int? Version { get; set; }
        public bool? Aprobado { get; set; }
        public DateTime? FechaRealCierre { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime? FechaProcesoPagoReal { get; set; }
        public DateTime? FechaIngresoEnCuenta { get; set; }
        public DateTime? FechaEfectivoDisponible { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
