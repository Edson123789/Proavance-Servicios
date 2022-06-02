using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/CriterioEvaluacionProceso
    /// Autor: Luis Huallpa - Edgar Serruto
    /// Fecha: 07/09/2021
    /// <summary>
    /// BO para T_CriterioEvaluacionProceso
    /// </summary>
    public class CriterioEvaluacionProcesoBO : BaseBO
    {
        /// Propiedades		    Significado
		/// -----------		    ------------
		/// Nombre              Nombre de Criterio de Evaluacionde Proceso
        public string Nombre { get; set; }
    }
}
