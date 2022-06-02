using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookCampanhaBO : BaseBO 
    {
        public string FacebookId { get; set; }
        public string Nombre { get; set; }
        public string Objetivo { get; set; }
        public string EstadoFacebook { get; set; }
        public int IdFacebookCuentaPublicitaria { get; set; }
        public int? IdMigracion { get; set; }
    }
}
