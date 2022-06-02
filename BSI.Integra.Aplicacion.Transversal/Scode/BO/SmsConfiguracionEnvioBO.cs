using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Transversal/SmsConfiguracionEnvioBO
    /// Autor: Gian Miranda
    /// Fecha: 09/12/2021
    /// <summary>
    /// BO para la configuracion de envio Sms
    /// </summary>
    public class SmsConfiguracionEnvioBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// Nombre                              Nombre de la configuracion Sms
        /// Descripcion                         Descripcion de la configuracion Sms
        /// IdPersonal                          Id del personal (PK de la tabla gp.T_Personal)
        /// IdPlantilla                         Id de la plantilla (PK de la tabla mkt.T_Plantilla)
        /// IdPGeneral                          Id del programa general (PK de la tabla pla.T_PGeneral)
        /// IdConjuntoListaDetalle              Id del conjunto lista detalle (PK de la tabla mkt.T_ConjuntoListaDetalle)
        /// Activo                              Flag para determinar si la configuracion esta activa
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPersonal { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public bool Activo { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public SmsConfiguracionEnvioBO()
        {
        }

        public SmsConfiguracionEnvioBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
