using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EsquemaEvaluacionPgeneralProveedorBO : BaseBO
    {
        public int IdEsquemaEvaluacionPgeneral { get; set; }
        public int IdProveedor { get; set; }
    }
}
