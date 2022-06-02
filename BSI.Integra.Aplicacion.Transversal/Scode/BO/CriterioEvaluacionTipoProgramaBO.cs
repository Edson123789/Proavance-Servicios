using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;


namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CriterioEvaluacionTipoProgramaBO : BaseBO
    {
        public int IdCriterioEvaluacion { get; set; }
        public int IdTipoPrograma { get; set; }
    }
}
