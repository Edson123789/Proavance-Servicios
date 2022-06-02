using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    /// BO: Marketing/MailchimpEstadisticaPorPrioridad
    /// Autor: Gian Miranda
    /// Fecha: 12/06/2021
    /// <summary>
    /// BO para la logica de estadistica por prioridad
    /// </summary>
    public class MailchimpEstadisticaPorPrioridadBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdPrioridadMailChimpLista           Id de la prioridad de la lista de MailChimp (PK de la tabla mkt.T_PrioridadMailChimpLista)
        /// CantidadCorreoAbierto               Cantidad de correos abiertos
        /// FechaConsulta                       Fecha de inicio de la captura de informacion
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int? IdPrioridadMailChimpLista { get; set; }
        public int CantidadCorreoAbierto { get; set; }
        public DateTime FechaConsulta { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;
        public MailchimpEstadisticaPorPrioridadBO()
        {
            _integraDBContext = new integraDBContext();
        }

        public MailchimpEstadisticaPorPrioridadBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
