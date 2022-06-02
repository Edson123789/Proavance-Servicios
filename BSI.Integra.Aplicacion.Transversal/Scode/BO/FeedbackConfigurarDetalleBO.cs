using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FeedbackConfigurarDetalleBO : BaseBO
    {
        public int IdFeedbackConfigurar { get; set; }
        public int IdSexo { get; set; }
        public int Puntaje { get; set; }
        public string NombreVideo { get; set; }
    }
}
