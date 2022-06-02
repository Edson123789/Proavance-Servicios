using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class PageFbValueBO : BaseEntity 
    {
        public string Verb { get; set; }
        public string Content { get; set; }
        public DateTime Fecha { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
