using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CriterioEvaluacionTipoPersonaBO : BaseBO
    {
        public int IdCriterioEvaluacion { get; set; }
        public int IdTipoPersona { get; set; }
        public int? IdMigracion { get; set; }
    }
}
