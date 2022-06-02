using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class DocumentoLegalPaisBO: BaseBO
    {
        public int IdDocumentoLegal { get; set; }
        public int IdPais { get; set; }
    }
}
