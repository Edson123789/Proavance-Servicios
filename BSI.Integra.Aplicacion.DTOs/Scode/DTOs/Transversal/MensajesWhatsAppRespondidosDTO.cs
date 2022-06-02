using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MensajesWhatsAppRespondidosDTO
    {
        public List<ResumenMensajesWhatsAppRespondidosDTO> ListaMensajesWhatsAppRespondidos { get; set; }
        public int Total { get; set; }
    }
}
