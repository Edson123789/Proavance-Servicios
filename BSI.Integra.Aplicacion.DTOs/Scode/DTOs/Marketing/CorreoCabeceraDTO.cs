using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CorreoCabeceraDTO
    {
        public string Remitente { get; set; }
        public string Destinatario { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public string DestinatarioCc { get; set; }
        public string DestinatarioBcc { get; set; }
        public string Usuario { get; set; }
        public string GrupoEmail { get; set; }
        public bool envioGrupo { get; set; }
    }
}
