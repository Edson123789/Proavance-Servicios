using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PreguntaFrecuenteAreaBO : BaseBO
    {
        public int? IdPreguntaFrecuente { get; set; }
        public int IdArea { get; set; }
    }
}
