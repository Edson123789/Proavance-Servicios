using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MessengerUsuarioBO : BaseBO
    {
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
        public Guid? IdMigracion { get; set; }
        public int? IdFacebookPagina { get; set; }
        public bool? Desuscrito { get; set; }

        private MessengerUsuarioRepositorio _repMessengerUsuario = new MessengerUsuarioRepositorio();
        public MessengerUsuarioBO()
        {

        }
        public MessengerUsuarioBO(string PSID , integraDBContext context)
        {
             _repMessengerUsuario = new MessengerUsuarioRepositorio();

            var obj = _repMessengerUsuario.FirstBy(a => a.Psid == PSID);
            if (obj != null)
            {
                this.Id = obj.Id;
                this.Psid = obj.Psid;
                this.Nombres = obj.Nombres;
                this.Apellidos = obj.Apellidos;
                this.UrlFoto = obj.UrlFoto;
                this.IdPersonal = obj.IdPersonal;
                this.SeRespondio = obj.SeRespondio;
                this.IdAreaCapacitacion = obj.IdAreaCapacitacion;
                this.IdAreaCapacitacionFacebook = obj.IdAreaCapacitacionFacebook;
                this.Telefono = obj.Telefono;
                this.Email = obj.Email;
                this.MensajeEnviarEmail = obj.MensajeEnviarEmail;
                this.MensajeEnviarTelefono = obj.MensajeEnviarTelefono;
                this.Estado = obj.Estado;
                this.FechaCreacion = obj.FechaCreacion;
                this.FechaModificacion = obj.FechaModificacion;
                this.UsuarioCreacion = obj.UsuarioCreacion;
                this.UsuarioModificacion = obj.UsuarioModificacion;
                this.RowVersion = obj.RowVersion;
            }            
        }
        public MessengerUsuarioBO(string PSID )
        {
            _repMessengerUsuario = new MessengerUsuarioRepositorio();
            var obj = _repMessengerUsuario.FirstBy(a => a.Psid == PSID);
            if (obj != null)
            {
                this.Id = obj.Id;
                this.Psid = obj.Psid;
                this.Nombres = obj.Nombres;
                this.Apellidos = obj.Apellidos;
                this.UrlFoto = obj.UrlFoto;
                this.IdPersonal = obj.IdPersonal;
                this.SeRespondio = obj.SeRespondio;
                this.IdAreaCapacitacion = obj.IdAreaCapacitacion;
                this.IdAreaCapacitacionFacebook = obj.IdAreaCapacitacionFacebook;
                this.Telefono = obj.Telefono;
                this.Email = obj.Email;
                this.MensajeEnviarEmail = obj.MensajeEnviarEmail;
                this.MensajeEnviarTelefono = obj.MensajeEnviarTelefono;
                this.Estado = obj.Estado;
                this.FechaCreacion = obj.FechaCreacion;
                this.FechaModificacion = obj.FechaModificacion;
                this.UsuarioCreacion = obj.UsuarioCreacion;
                this.UsuarioModificacion = obj.UsuarioModificacion;
                this.RowVersion = obj.RowVersion;
            }            
        }
    }
}
