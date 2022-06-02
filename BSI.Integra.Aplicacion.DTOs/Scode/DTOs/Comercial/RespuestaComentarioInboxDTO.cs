using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RespuestaComentarioInboxDTO
    {
        public string IdCommentFacebook { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
        public int IdPersonal { get; set; }

    }
}
