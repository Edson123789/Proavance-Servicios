using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    /// BO: GestionPersonas/EvaluacionTipo
    /// Autor: Britsel Calluchi
    /// Fecha: 08/09/2021
    /// <summary>
    /// BO para T_EvaluacionTipo
    /// </summary>
    public class EvaluacionTipoBO : BaseBO
    {
        /// Propiedades		    Significado
        /// -----------		    ------------
        /// Nombre			    Nombre de Tipo de Evaluación
        public string Nombre { get; set; }
    }
}
