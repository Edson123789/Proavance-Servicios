using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class DocumentoLegalAreaTrabajoBO: BaseBO
    {
        public int IdDocumentoLegal { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
