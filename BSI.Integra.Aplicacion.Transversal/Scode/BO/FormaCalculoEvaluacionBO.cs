using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FormaCalculoEvaluacionBO : BaseBO
    {
        public string Nombre { get; set; }
        public bool EsSuma { get; set; }
        public bool EsPromedio { get; set; }
    }
}
