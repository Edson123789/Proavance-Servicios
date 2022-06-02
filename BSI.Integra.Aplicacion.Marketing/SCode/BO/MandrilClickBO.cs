using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class MandrilClickBO : BaseBO
    {
        public int IdMandril { get; set; }
        public string Ip { get; set; }
        public string Ubicacion { get; set; }
        public DateTime Ts { get; set; }
        public string UserAgent { get; set; }
        public string Url { get; set; }

        public MandrilClickBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}
