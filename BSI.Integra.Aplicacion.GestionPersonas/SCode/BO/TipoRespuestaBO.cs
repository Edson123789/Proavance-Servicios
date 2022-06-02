using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/TipoRespuesta
    /// Autor: Britsel Calluchi
    /// Fecha: 07/09/2021
    /// <summary>
    /// BO para T_TipoRespuesta
    /// </summary>
    public class TipoRespuestaBO: BaseBO
    {
        /// Propiedades						Significado
        /// -----------						------------
        /// Nombre							Nombre de Tipo de Respuesta
        /// Descripcion                     Descripción de Tipo de Respuesta
        /// IdMigracion						Id de Migración
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
