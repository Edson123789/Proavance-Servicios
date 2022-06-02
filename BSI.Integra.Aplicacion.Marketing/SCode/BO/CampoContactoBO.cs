using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CampoContactoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string TipoControl { get; set; }
        public int ValoresPreEstablecidos { get; set; }
        public string Procedimiento { get; set; }
    }
}
