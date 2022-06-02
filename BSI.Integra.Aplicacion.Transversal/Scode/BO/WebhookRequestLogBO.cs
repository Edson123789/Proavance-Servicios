using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class WebHookRequestLogBO : BaseBO
    {
        public string Verb { get; set; }
        public string Content { get; set; }
        public DateTime Fecha { get; set; }
        public WebHookRequestLogBO()
        {
        }
    }
}
