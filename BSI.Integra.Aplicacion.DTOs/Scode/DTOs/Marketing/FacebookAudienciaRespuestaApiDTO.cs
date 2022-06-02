using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FacebookAudienciaRespuestaApiDTO
    {
        public bool FlagAudiencia { get; set; }
        public bool FlagEmails { get; set; }
        public int EmailsRecibidos { get; set; }
        public string FacebookIdAudiencia { get; set; }
        public string MensajeError { get; set; }
    }
}
