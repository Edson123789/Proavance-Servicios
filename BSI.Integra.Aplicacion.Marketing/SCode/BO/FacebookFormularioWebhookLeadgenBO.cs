using System;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookFormularioWebhookLeadgenBO : BaseBO
    {
        public string FacebookIdLeadgen { get; set; }
        public string FacebookIdCampania { get; set; }
        public string FacebookIdFormulario { get; set; }
        public string FacebookFechaUnix { get; set; }
        public string FacebookIdPagina { get; set; }
        public string FacebookIdGrupo { get; set; }
        public bool EsProcesado { get; set; }
        public DateTime FacebookFechaLead { get; set; }
    }
}
