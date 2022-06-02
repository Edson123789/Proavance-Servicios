using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CampoEtiquetaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Campo { get; set; }
        public int IdAreaCampoEtiqueta { get; set; }
        public int IdSubAreaCampoEtiqueta { get; set; }
    }
}
