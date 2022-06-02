using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWhatsAppEstadoValidacion
    {
        public TWhatsAppEstadoValidacion()
        {
            TWhatsAppMensajePublicidad = new HashSet<TWhatsAppMensajePublicidad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TWhatsAppMensajePublicidad> TWhatsAppMensajePublicidad { get; set; }
    }
}
