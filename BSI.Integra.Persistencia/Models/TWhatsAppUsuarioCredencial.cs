using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppUsuarioCredencial
    {
        public int Id { get; set; }
        public int IdWhatsAppUsuario { get; set; }
        public int IdWhatsAppConfiguracion { get; set; }
        public string UserAuthToken { get; set; }
        public DateTime? ExpiresAfter { get; set; }
        public bool? EsMigracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TWhatsAppConfiguracion IdWhatsAppConfiguracionNavigation { get; set; }
        public virtual TWhatsAppUsuario IdWhatsAppUsuarioNavigation { get; set; }
    }
}
