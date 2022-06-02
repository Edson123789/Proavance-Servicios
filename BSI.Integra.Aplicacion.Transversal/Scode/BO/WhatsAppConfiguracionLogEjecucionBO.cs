using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WhatsAppConfiguracionLogEjecucionBO : BaseBO
    {
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        private readonly integraDBContext _integraDBContext;

        public WhatsAppConfiguracionLogEjecucionBO()
        {
        }

        public WhatsAppConfiguracionLogEjecucionBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
