using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoDetallePartnerBO : BaseBO
    {
        public int IdCertificadoDetalle { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string ContentType { get; set; }
        public int? IdMigracion { get; set; }
    }
}
