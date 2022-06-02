using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ErrorBO : BaseBO
    {
        public int Codigo { get; set; }
        public int IdErrorTipo { get; set; }
        public string Descripcion { get; set; }
    }
}
