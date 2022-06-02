using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing
{
    public class ControlMensajeChatAsesorDTO
    {
        public int Id { get; set; }
        public int IdOrigenMensajeChat { get; set; }
        public int IdPersonal { get; set; }
        public string DatosEmisor { get; set; }
        public string Mensaje { get; set; }
        public string  EstadoMensaje { get; set; }
        public string Usuario { get; set; }
    }

    public class ControlMensajeChatAsesorFiltroDTO
    {
        public int Id { get; set; }
        public string OrigenMensajeChat { get; set; }
        public string Personal { get; set; }
        public string DatosEmisor { get; set; }
        public string Mensaje { get; set; }
        public string EstadoMensaje { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class ListaMensajeChatDTO
    {
        public List<int> ListaMensajesPortal { get; set; }
        public List<int> ListaMensajesWhatsApp { get; set; }
        public string Usuario { get; set; }
    }
}
