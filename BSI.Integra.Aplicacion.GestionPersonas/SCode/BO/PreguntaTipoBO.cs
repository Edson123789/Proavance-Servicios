using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/PreguntaTipo
    /// Autor: Britsel Calluchi
    /// Fecha: 08/09/2021
    /// <summary>
    /// BO para T_PreguntaTipo
    /// </summary>
    public class PreguntaTipoBO : BaseBO
    {
        /// Propiedades		    Significado
        /// -----------		    ------------
        /// Nombre			    Nombre de Tipo de Pregunta
        /// IdTipoRespuesta     Id de Tipo de Respuesta Fk de T_TipoRespuesta
        /// IdMigracion		    Id de Migración
        public string Nombre { get; set; }
        public int IdTipoRespuesta { get; set; }
        public int? IdMigracion { get; set; }
    }
}
