using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class FacebookPostBO : BaseBO
    {
        public string Link { get; set; }
        public string Message { get; set; }
        public string PermalinkUrl { get; set; }
        public string UrlPicture { get; set; }
        public string IdPostFacebook { get; set; }
        public int? IdPGeneral { get; set; }
        public string ConjuntoAnuncioIdFacebook { get; set; }
        public string IdAnuncioFacebook { get; set; }
        public string CentroCosto { get; set; }
        public string CentroCosto2 { get; set; }

        public FacebookPostBO()
        {
        }
    }
}
