using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CriterioEvaluacionBO : BaseBO 
    {
        
        public string Nombre { get; set; }
        public List<CriterioEvaluacionTipoProgramaBO> IdTipoPrograma { get; set; }
        public List<CriterioEvaluacionModalidadCursoBO> IdModalidadCurso { get; set; }
        public List<CriterioEvaluacionTipoPersonaBO> IdTipoPersona { get; set; }
        public int IdCriterioEvaluacionCategoria { get; set; }

        public int? IdFormaCalculoEvaluacion { get; set; }
        public int? IdFormaCalificacionEvaluacion { get; set; }
        public int? IdFormaCalculoEvaluacionParametro { get; set; }

        public List<ParametroEvaluacionBO> ListadoParametro { get; set; }
    }
}
