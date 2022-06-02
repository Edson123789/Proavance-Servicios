using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppConfiguracion
    {
        public TWhatsAppConfiguracion()
        {
            TWhatsAppUsuarioCredencial = new HashSet<TWhatsAppUsuarioCredencial>();
        }

        public int Id { get; set; }
        public string UrlWhatsApp { get; set; }
        public string IpHost { get; set; }
        public string Numero { get; set; }
        public string Vname { get; set; }
        public string Certificado { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPais IdPaisNavigation { get; set; }
        public virtual ICollection<TWhatsAppUsuarioCredencial> TWhatsAppUsuarioCredencial { get; set; }
    }
}
