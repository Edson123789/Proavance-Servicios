using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppConfiguracionEnvio
    {
        public TWhatsAppConfiguracionEnvio()
        {
            TWhatsAppConfiguracionEnvioPorPrograma = new HashSet<TWhatsAppConfiguracionEnvioPorPrograma>();
            TWhatsAppConfiguracionLogEjecucion = new HashSet<TWhatsAppConfiguracionLogEjecucion>();
            TWhatsAppMensajePublicidad = new HashSet<TWhatsAppMensajePublicidad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public DateTime? FechaDesactivacion { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdCampaniaGeneralDetalle { get; set; }

        public virtual TConjuntoListaDetalle IdConjuntoListaDetalleNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TPlantilla IdPlantillaNavigation { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionEnvioPorPrograma> TWhatsAppConfiguracionEnvioPorPrograma { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionLogEjecucion> TWhatsAppConfiguracionLogEjecucion { get; set; }
        public virtual ICollection<TWhatsAppMensajePublicidad> TWhatsAppMensajePublicidad { get; set; }
    }
}
