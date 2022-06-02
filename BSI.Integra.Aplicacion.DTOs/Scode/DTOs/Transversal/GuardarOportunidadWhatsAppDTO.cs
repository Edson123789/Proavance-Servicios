using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GuardarOportunidadWhatsAppDTO
    {
        public int IdWhatsAppConfiguracionEnvioDetalle { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPersonal { get; set; }
        public string NombreUsuario { get; set; }
    }
}
