using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppUsuarioCredencialBO : BaseBO
    {
        public int IdWhatsAppUsuario { get; set; }
        public int IdWhatsAppConfiguracion { get; set; }
        public string UserAuthToken { get; set; }
        public DateTime? ExpiresAfter { get; set; }
        public bool? EsMigracion { get; set; }

        public int? IdMigracion { get; set; }
    }
}
