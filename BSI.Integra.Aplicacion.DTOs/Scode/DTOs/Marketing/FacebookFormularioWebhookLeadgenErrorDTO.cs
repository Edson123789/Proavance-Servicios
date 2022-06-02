using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FacebookFormularioWebhookLeadgenErrorDTO
    {
        public string FacebookIdLead { get; set; }
        public string FacebookIdCampania { get; set; }
        public string FacebookIdFormulario { get; set; }
        public string FacebookFechaUnix { get; set; }
        public string FacebookIdPagina { get; set; }
        public string FacebookIdGrupo { get; set; }
        public bool EsProcesado { get; set; }
        public string ErrorReal { get; set; }
        public string Error { get; set; }
    }
}
