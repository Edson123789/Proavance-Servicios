using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReaccionPublicacionWebHookFacebookDTO
    {
        public string UsuarioFacebookId { get; set; }
        public string IdPostFacebook { get; set; }
        public string Accion { get; set; }
        public string TipoReaccion { get; set; }
    }
}
