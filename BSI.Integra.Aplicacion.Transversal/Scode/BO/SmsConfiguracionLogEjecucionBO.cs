using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/SmsConfiguracionLogEjecucionBO
    /// Autor: Gian Miranda
    /// Fecha: 09/12/2021
    /// <summary>
    /// BO para el log de ejecucion de la configuracion de envio Sms
    /// </summary>
    public class SmsConfiguracionLogEjecucionBO : BaseBO
    {
        public int IdSmsConfiguracionEnvio { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public SmsConfiguracionLogEjecucionBO()
        {
        }

        public SmsConfiguracionLogEjecucionBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
