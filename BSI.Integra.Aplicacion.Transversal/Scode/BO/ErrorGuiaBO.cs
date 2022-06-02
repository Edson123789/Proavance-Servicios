using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ErrorGuiaBO
    {
        public bool Error { get; set; }
        public string InnerException { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string Descripcion { get; set; }
    }
}
