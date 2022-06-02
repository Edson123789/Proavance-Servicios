using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PreguntaEvaluacionTrabajoBO : BaseBO
    {
        public int IdConfigurarEvaluacionTrabajo { get; set; }
        public int IdPregunta { get; set; }
    }

    public class registroPreguntaEvaluacionTrabajoBO
    {
        public int IdConfigurarEvaluacionTrabajo { get; set; }
        public int IdPregunta { get; set; }
    }
}
