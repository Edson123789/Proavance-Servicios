using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TNuevoAlumnoCongelado
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal? TotalPagar { get; set; }
        public decimal Cuota { get; set; }
        public decimal Saldo { get; set; }
        public decimal Mora { get; set; }
        public decimal MontoPagado { get; set; }
        public bool Cancelado { get; set; }
        public string TipoCuota { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaPago { get; set; }
        public DateTime FechaCongelamiento { get; set; }
        public int IdPeriodo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
