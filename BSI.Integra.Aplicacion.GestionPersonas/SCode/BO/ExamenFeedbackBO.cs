using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/ExamenFeedback
    /// Autor: Britsel Calluchi
    /// Fecha: 07/09/2021
    /// <summary>
    /// BO para T_ExamenFeedback
    /// </summary>
    public class ExamenFeedbackBO : BaseBO
    {
        /// Propiedades		    Significado
        /// -----------		    ------------
        /// Nombre			    Nombre de Feedback de Examen
        /// Url                 Url de Feedback
        /// IdMigracion		    Id de Migración
        public string Nombre { get; set; }
        public string Url { get; set; }
        public int? IdMigracion { get; set; }
    }
}
