using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FeedbackGrupoPreguntaProgramaEspecificoBO : BaseBO
    {
        public int IdFeedbackConfigurarGrupoPregunta { get; set; }
        public int IdPespecifico { get; set; }
    }
}
