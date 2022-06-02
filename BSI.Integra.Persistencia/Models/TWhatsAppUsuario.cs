using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppUsuario
    {
        public TWhatsAppUsuario()
        {
            TWhatsAppUsuarioCredencial = new HashSet<TWhatsAppUsuarioCredencial>();
        }

        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public string RolUser { get; set; }
        public string UserUsername { get; set; }
        public string UserPassword { get; set; }
        public bool? EsMigracion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual ICollection<TWhatsAppUsuarioCredencial> TWhatsAppUsuarioCredencial { get; set; }
    }
}
