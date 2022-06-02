using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialEnvio
    {
        public TMaterialEnvio()
        {
            TMaterialEnvioDetalle = new HashSet<TMaterialEnvioDetalle>();
        }

        public int Id { get; set; }
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdProveedorEnvio { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TMaterialEnvioDetalle> TMaterialEnvioDetalle { get; set; }
    }
}
