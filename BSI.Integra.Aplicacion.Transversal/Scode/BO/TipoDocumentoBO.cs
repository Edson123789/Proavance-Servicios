using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoDocumentoBO : BaseBO
    {
        public int Clave { get; set; }
        public string Descripcion { get; set; }
    }
}
