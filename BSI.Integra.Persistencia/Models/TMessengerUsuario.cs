using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMessengerUsuario
    {
        public TMessengerUsuario()
        {
            TMessengerUsuarioLog = new HashSet<TMessengerUsuarioLog>();
        }

        public int Id { get; set; }
        public string Psid { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UrlFoto { get; set; }
        public int? IdPersonal { get; set; }
        public bool? SeRespondio { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool? MensajeEnviarTelefono { get; set; }
        public bool? MensajeEnviarEmail { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdFacebookPagina { get; set; }
        public bool? Desuscrito { get; set; }

        public virtual ICollection<TMessengerUsuarioLog> TMessengerUsuarioLog { get; set; }
    }
}
