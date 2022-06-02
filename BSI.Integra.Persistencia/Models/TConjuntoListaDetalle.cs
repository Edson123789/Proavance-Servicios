using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConjuntoListaDetalle
    {
        public TConjuntoListaDetalle()
        {
            TConjuntoListaDetalleValor = new HashSet<TConjuntoListaDetalleValor>();
            TConjuntoListaResultado = new HashSet<TConjuntoListaResultado>();
            TSmsConfiguracionEnvio = new HashSet<TSmsConfiguracionEnvio>();
            TWhatsAppConfiguracionEnvio = new HashSet<TWhatsAppConfiguracionEnvio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdConjuntoLista { get; set; }
        public byte Prioridad { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TConjuntoLista IdConjuntoListaNavigation { get; set; }
        public virtual ICollection<TConjuntoListaDetalleValor> TConjuntoListaDetalleValor { get; set; }
        public virtual ICollection<TConjuntoListaResultado> TConjuntoListaResultado { get; set; }
        public virtual ICollection<TSmsConfiguracionEnvio> TSmsConfiguracionEnvio { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvio> TWhatsAppConfiguracionEnvio { get; set; }
    }
}
