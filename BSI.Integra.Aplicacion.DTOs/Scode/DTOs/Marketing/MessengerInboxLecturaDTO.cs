using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerInboxLecturaDTO
    {
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public DateTime FechaInteraccion { get; set; }
        public DateTime FechaLectura { get; set; }
    }
}
