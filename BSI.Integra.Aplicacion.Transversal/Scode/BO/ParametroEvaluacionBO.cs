using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ParametroEvaluacionBO : BaseBO
    {
        public int IdCriterioEvaluacion { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public string Nombre { get; set; }
        public int Ponderacion { get; set; }
    }
}
