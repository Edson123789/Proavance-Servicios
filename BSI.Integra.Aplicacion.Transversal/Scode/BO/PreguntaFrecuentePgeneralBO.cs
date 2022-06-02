using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PreguntaFrecuentePgeneralBO : BaseBO
    {
        public int? IdPreguntaFrecuente { get; set; }
        public int? IdPgeneral { get; set; }
    }
}
