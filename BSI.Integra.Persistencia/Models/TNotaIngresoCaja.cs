using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TNotaIngresoCaja
    {
        public int Id { get; set; }
        public int IdCaja { get; set; }
        public string CodigoNic { get; set; }
        public int IdOrigenIngresoCaja { get; set; }
        public int IdPersonalEmitido { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaGiro { get; set; }
        public string Concepto { get; set; }
        public DateTime FechaCobro { get; set; }
        public int Anho { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
