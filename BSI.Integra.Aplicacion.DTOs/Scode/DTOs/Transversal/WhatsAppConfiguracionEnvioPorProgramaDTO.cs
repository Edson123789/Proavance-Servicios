using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class WhatsAppConfiguracionEnvioPorProgramaDTO
    {
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPgeneral { get; set; }
        public int IdTipoEnvioPrograma { get; set; }
    }

    public class SmsConfiguracionEnvioPorProgramaDTO
    {
        public int IdSmsConfiguracionEnvio { get; set; }
        public int IdPGeneral { get; set; }
        public int IdTipoEnvioPrograma { get; set; }
    }
}
