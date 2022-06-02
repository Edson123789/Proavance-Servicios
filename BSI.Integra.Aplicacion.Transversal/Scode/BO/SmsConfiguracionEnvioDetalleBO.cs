using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/SmsConfiguracionEnvioDetalleBO
    /// Autor: Gian Miranda
    /// Fecha: 09/12/2021
    /// <summary>
    /// BO para el detalle de la configuracion de envio Sms
    /// </summary>
    public class SmsConfiguracionEnvioDetalleBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdSmsConfiguracionLogEjecucion      Id del log de ejecucion de la configuracion Sms (PK de la tabla mkt.T_SmsConfiguracionLogEjecucion)
        /// EnviadoCorrectamente                Flag para determinar si se envio correctamente el Sms
        /// MensajeError                        Mensaje del error presente en el envio del Sms
        /// IdConjuntoListaResultado            Id del resultado del conjunto lista (PK de la tabla mkt.T_ConjuntoListaResultado)
        /// ConjuntoListaNroEjecucion           Numero de ejecucion del conjunto lista
        /// Mensaje                             Mensaje enviado al destinatario
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int IdSmsConfiguracionLogEjecucion { get; set; }
        public bool EnviadoCorrectamente { get; set; }
        public string MensajeError { get; set; }
        public int? IdConjuntoListaResultado { get; set; }
        public int? ConjuntoListaNroEjecucion { get; set; }
        public string Mensaje { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public SmsConfiguracionEnvioDetalleBO()
        {
        }

        public SmsConfiguracionEnvioDetalleBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
