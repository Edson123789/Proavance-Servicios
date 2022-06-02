using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ApiGraphFacebookResponseMensajeArchivoAdjuntoDTO
    {
        public string recipient_id { get; set; }
        public string message_id { get; set; }
        public string attachment_id { get; set; }
    }
}
