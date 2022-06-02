using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTOs 
{ 
    public class WhatsAppBase
    {
    }

    public class mensajeError
    {
        public meta meta { get; set; }
        public errors[] errors { get; set; }
    }

    public class meta
    {
        public string version { get; set; }
        public string api_status { get; set; }
    }

    public class errors
    {
        public string code { get; set; }
        public string title { get; set; }
        public string details { get; set; }
    }
}
