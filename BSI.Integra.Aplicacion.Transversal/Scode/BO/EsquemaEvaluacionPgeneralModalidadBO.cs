using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EsquemaEvaluacionPgeneralModalidadBO : BaseBO
    {
        public int IdEsquemaEvaluacionPgeneral { get; set; }
        public int IdModalidadCurso { get; set; }
    }
}
