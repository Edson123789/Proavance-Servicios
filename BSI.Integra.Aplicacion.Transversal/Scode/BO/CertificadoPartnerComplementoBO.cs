using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CertificadoPartnerComplementoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string FrontalCentral { get; set; }
        public string FrontalInferiorIzquierda { get; set; }
        public string PosteriorCentral { get; set; }
        public string PosteriorInferiorIzquierda { get; set; }
        public string MencionEnCertificado { get; set; }
        public int? IdMigracion { get; set; }
    }
}
