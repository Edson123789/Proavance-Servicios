using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class HistorialChatRecibidosDTO
    {
        public int IdInteraccionChat { get; set;}
        public string NombreRemitente { get; set;}
        public string Ubicacion { get; set;}
        public string Mensaje {get; set;}
        public int IdAsesor {get; set;}
        public DateTime? Fecha { get; set; }
        public string Chatsession { get; set; }
    }
}
