using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConjuntoListaResultado
    {
        public TConjuntoListaResultado()
        {
            TSmsConfiguracionEnvioDetalle = new HashSet<TSmsConfiguracionEnvioDetalle>();
            TWhatsAppConfiguracionEnvioDetalle = new HashSet<TWhatsAppConfiguracionEnvioDetalle>();
        }

        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public bool? EsVentaCruzada { get; set; }
        public int NroEjecucion { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdOportunidad { get; set; }

        public virtual TConjuntoListaDetalle IdConjuntoListaDetalleNavigation { get; set; }
        public virtual ICollection<TSmsConfiguracionEnvioDetalle> TSmsConfiguracionEnvioDetalle { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvioDetalle> TWhatsAppConfiguracionEnvioDetalle { get; set; }
    }
}
