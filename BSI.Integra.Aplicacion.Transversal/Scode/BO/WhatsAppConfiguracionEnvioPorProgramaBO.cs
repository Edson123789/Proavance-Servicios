using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppConfiguracionEnvioPorProgramaBO : BaseBO
    {
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPgeneral { get; set; }
        public int IdTipoEnvioPrograma { get; set; }        
        public int? IdMigracion { get; set; }
    }
}
