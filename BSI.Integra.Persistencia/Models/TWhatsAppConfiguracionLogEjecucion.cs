using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppConfiguracionLogEjecucion
    {
        public TWhatsAppConfiguracionLogEjecucion()
        {
            TWhatsAppConfiguracionEnvioDetalle = new HashSet<TWhatsAppConfiguracionEnvioDetalle>();
        }

        public int Id { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TWhatsAppConfiguracionEnvio IdWhatsAppConfiguracionEnvioNavigation { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvioDetalle> TWhatsAppConfiguracionEnvioDetalle { get; set; }
    }
}
