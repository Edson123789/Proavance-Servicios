using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ChatComentarioPersonalDTO
    {
        public List<MessengerChatDTO> ListaChatMessenger { get; set; }
        public List<PostComentarioUsuarioCompuestoDTO> ListaComentarioMessenger { get; set; }
    }
}
