using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FacebookPaginaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string FacebookId { get; set; }
        public int? IdMigracion { get; set; }
    }
}
