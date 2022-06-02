using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ExamenComportamientoBO : BaseBO
    {
        public bool PreguntaObligatoria { get; set; }
        public int? IdEvaluacionFeedbackAprobado { get; set; }
        public int? IdEvaluacionFeedbackDesaprobado { get; set; }
        public int? IdEvaluacionFeedbackCancelado { get; set; }
        public int? IdMigracion { get; set; }
    }
}
