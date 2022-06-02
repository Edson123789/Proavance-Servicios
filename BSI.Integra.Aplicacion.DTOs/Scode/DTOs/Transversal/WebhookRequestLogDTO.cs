using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WebhookRequestLogDTO
    {
        public string Verb { get; set; }
        public string Content { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
