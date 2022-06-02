using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.DTOs
{
    public class TMKMailDataDTO
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public List<Mandrill.Models.EmailAttachment> AttachedFiles { get; set; }
        public List<Mandrill.Models.Image> EmbeddedFiles { get; set; }
        public string RemitenteC { get; set; }
    }

    public class FechaIntervaloMailingDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
