using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CentroCostoCertificadoBO : BaseBO
    {
        public int IdCentroCosto { get; set; }
        public int? IdCertificadoBrochure { get; set; }
        public int? IdCertificadoPartnerComplemento { get; set; }
        public int? IdMigracion { get; set; }
    }
}
