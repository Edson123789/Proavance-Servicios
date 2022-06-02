using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoComprobanteDetalle
    {
        public int Id { get; set; }
        public int IdTipoComprobante { get; set; }
        public string Campo { get; set; }
        public int CantidadCaracteres { get; set; }
        public int TipoInput { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
