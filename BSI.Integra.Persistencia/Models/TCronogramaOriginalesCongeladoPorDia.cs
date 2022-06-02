using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaOriginalesCongeladoPorDia
    {
        public int Id { get; set; }
        public string EstadoMatricula { get; set; }
        public string Alumno { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public double? Cuota { get; set; }
        public int? NroCuota { get; set; }
        public int? NroSubCuota { get; set; }
        public string Moneda { get; set; }
        public double? CuotaDolares { get; set; }
        public string CodigoMatricula { get; set; }
        public string PeriodoPorFechaVencimiento { get; set; }
        public string CoordinadoraAcademica { get; set; }
        public string CoordinadoraCobranza { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaCongelamiento { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public int? IdMigracion { get; set; }
        public byte[] RowVersion { get; set; }
        public double? MontoPagado { get; set; }
    }
}
