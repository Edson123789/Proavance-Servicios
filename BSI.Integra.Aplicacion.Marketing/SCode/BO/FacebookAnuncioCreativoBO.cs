using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookAnuncioCreativoBO : BaseBO
    {
        public string FacebookId { get; set; }
        public int IdPaginaFacebook { get; set; }
        public string TipoObjetivo { get; set; }
        public string Mensaje { get; set; }
        public int IdFacebookCuentaPublicitaria { get; set; }
        public int? IdMigracion { get; set; }
    }
}
