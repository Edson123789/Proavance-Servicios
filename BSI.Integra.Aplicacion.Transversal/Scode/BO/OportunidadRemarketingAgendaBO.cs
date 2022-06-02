using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/OportunidadRemarketingAgenda
    /// Autor: Gian Miranda
    /// Fecha: 09/09/2021
    /// <summary>
    /// BO para la logica de las oportunidades remarketing agenda
    /// </summary>
    public class OportunidadRemarketingAgendaBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// IdOportunidad                       Id de la oportunidad (PK de la tabla com.T_Oportunidad)
        /// IdAgendaTab                         Id del tab de la agenda (PK de la tabla com.T_AgendaTab)
        /// AplicaRedireccion                   Flag para determinar si aplica la redireccion de la agenda
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public int IdOportunidad { get; set; }
        public int IdAgendaTab { get; set; }
        public bool AplicaRedireccion { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public OportunidadRemarketingAgendaBO()
        {
        }

        public OportunidadRemarketingAgendaBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
