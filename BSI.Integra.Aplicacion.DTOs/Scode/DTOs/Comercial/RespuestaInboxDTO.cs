using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RespuestaInboxDTO
    {
        public string PSID { get; set; }
        public string Mensaje { get; set; }
        public string TipoArchivoAdjunto { get; set; }
        public string Usuario { get; set; }
        public int IdPersonal { get; set; }
        public string RecipientId { get; set; }
    }
}
