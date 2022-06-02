using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPespecificoConsumo
    {
        public int Id { get; set; }
        public int? IdPespecificoSesion { get; set; }
        public int? IdHistoricoProductoProveedor { get; set; }
        public decimal Cantidad { get; set; }
        public string Factor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
