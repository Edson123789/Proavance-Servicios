using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPais
    {
        public TPais()
        {
            TPlantillaPais = new HashSet<TPlantillaPais>();
            TWhatsAppConfiguracion = new HashSet<TWhatsAppConfiguracion>();
            TWhatsAppConfiguracionPreEnvio = new HashSet<TWhatsAppConfiguracionPreEnvio>();
            TWhatsAppEstadoMensajeEnviado = new HashSet<TWhatsAppEstadoMensajeEnviado>();
            TWhatsAppMensajeEnviado = new HashSet<TWhatsAppMensajeEnviado>();
            TWhatsAppMensajeRecibido = new HashSet<TWhatsAppMensajeRecibido>();
            TWhatsAppObjetoSerealizado = new HashSet<TWhatsAppObjetoSerealizado>();
        }

        public int Id { get; set; }
        public int CodigoPais { get; set; }
        public string CodigoIso { get; set; }
        public string NombrePais { get; set; }
        public string Moneda { get; set; }
        public decimal ZonaHoraria { get; set; }
        public int EstadoPublicacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? CodigoGoogleId { get; set; }
        public string CodigoPaisMoodle { get; set; }
        public string RutaBandera { get; set; }
        public string RutaIcono { get; set; }

        public virtual ICollection<TPlantillaPais> TPlantillaPais { get; set; }
        public virtual ICollection<TWhatsAppConfiguracion> TWhatsAppConfiguracion { get; set; }
        public virtual ICollection<TWhatsAppConfiguracionPreEnvio> TWhatsAppConfiguracionPreEnvio { get; set; }
        public virtual ICollection<TWhatsAppEstadoMensajeEnviado> TWhatsAppEstadoMensajeEnviado { get; set; }
        public virtual ICollection<TWhatsAppMensajeEnviado> TWhatsAppMensajeEnviado { get; set; }
        public virtual ICollection<TWhatsAppMensajeRecibido> TWhatsAppMensajeRecibido { get; set; }
        public virtual ICollection<TWhatsAppObjetoSerealizado> TWhatsAppObjetoSerealizado { get; set; }
    }
}
