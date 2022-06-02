using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSmsConfiguracionLogEjecucion
    {
        public TSmsConfiguracionLogEjecucion()
        {
            TSmsConfiguracionEnvioDetalle = new HashSet<TSmsConfiguracionEnvioDetalle>();
        }

        public int Id { get; set; }
        public int IdSmsConfiguracionEnvio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TSmsConfiguracionEnvioDetalle> TSmsConfiguracionEnvioDetalle { get; set; }
    }
}
