using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EsquemaEvaluacionDetalleBO : BaseBO
    {
        public int IdEsquemaEvaluacion { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int Ponderacion { get; set; }
    }
}
