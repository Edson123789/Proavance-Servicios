using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerInboxEchoDTO
    {
        public string SenderId { get; set; }
        public string Mensaje { get; set; }
        public string FacebookId { get; set; }
        public string UrlArchivoAdjunto { get; set; }
        public int IdTipoMensajeMessenger { get; set; }
        public DateTime FechaInteraccion { get; set; }
        public string RecipientId { get; set; }
    }
}
