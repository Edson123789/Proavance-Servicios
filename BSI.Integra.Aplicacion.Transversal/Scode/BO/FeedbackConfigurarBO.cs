using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FeedbackConfigurarBO : BaseBO
    {
        public int IdFeedbackTipo { get; set; }
        public string Nombre { get; set; }
    }
}
