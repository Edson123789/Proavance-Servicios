using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookAnuncioBO : BaseBO
    {
        public string FacebookId { get; set; }
        public string Nombre { get; set; }
        public int IdConjuntoAnuncioFacebook { get; set; }
        public int IdFacebookAnuncioCreativo { get; set; }
        public int IdFacebookCuentaPublicitaria { get; set; }
        public int? IdMigracion { get; set; }
    }
}
