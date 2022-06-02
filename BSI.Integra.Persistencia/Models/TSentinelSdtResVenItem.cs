using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSentinelSdtResVenItem
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public int? CantidadDocs { get; set; }
        public string Fuente { get; set; }
        public string Entidad { get; set; }
        public decimal? Monto { get; set; }
        public short? Cantidad { get; set; }
        public int? DiasVencidos { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TSentinel IdSentinelNavigation { get; set; }
    }
}
