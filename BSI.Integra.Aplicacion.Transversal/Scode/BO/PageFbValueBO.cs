using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PageFbValueBO : BaseBO
    {
        public string Verb { get; set; }
        public string Content { get; set; }
        public DateTime Fecha { get; set; }

        public PageFbValueBO() { }
    }
}
