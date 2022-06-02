using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoReversionEmisionBO : BaseBO
    {
        public int IdCertificadoDetalle { get; set; }
        public int? IdMigracion { get; set; }
    }
}
