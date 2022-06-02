using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReporteFlujoCongeladoPorDia
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CoordinadorAcademico { get; set; }
        public string CoordinadorCobranza { get; set; }
        public int IdPespecifico { get; set; }
        public string NombrePrograma { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoCuota { get; set; }
        public decimal TotalPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public DateTime? FechaProcesoPago { get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal SaldoPendienteDolar { get; set; }
        public decimal TotalCuotaDolar { get; set; }
        public decimal RealPagoDolar { get; set; }
        public string NroDocumento { get; set; }
        public string MonedaPago { get; set; }
        public decimal? TipoCambio { get; set; }
        public decimal Mora { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public int Version { get; set; }
        public bool Cancelado { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public DateTime FechaCongelamiento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
