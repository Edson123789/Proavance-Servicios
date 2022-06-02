using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/SmsMensajeEnviado
    /// Autor: Gian Miranda
    /// Fecha: 31/12/2021
    /// <summary>
    /// BO para la logica de los SMS enviados
    /// </summary>
    public class SmsMensajeEnviadoBO : BaseBO
    {
        /// Propiedades	                        Significado
        /// -----------	                        ------------
        /// Celular                             Celular al cual se envia el mensaje
        /// IdPersonal                          Id del personal (PK de la tabla gp.T_Personal)
        /// IdAlumno                            Id del alumno (PK de la tabla mkt.T_Alumno)
        /// Mensaje                             Mensaje entrante
        /// ParteMensaje                        Parte del mensaje seccionado
        /// IdPais                              Id del pais (PK de la tabla conf.T_Pais)
        /// IdMigracion                         Id migracion de V3 (Campo nullable)

        public string Celular { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
        public string Mensaje { get; set; }
        public int ParteMensaje { get; set; }
        public int? IdPais { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public SmsMensajeEnviadoBO()
        {
        }

        public SmsMensajeEnviadoBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
