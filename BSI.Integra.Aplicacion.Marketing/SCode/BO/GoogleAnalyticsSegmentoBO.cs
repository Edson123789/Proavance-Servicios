using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class GoogleAnalyticsSegmentoBO : BaseBO
    {
        public string Nombre { get; set; }
        public string NombreIngles { get; set; }
        public int GoogleAnalyticsId { get; set; }
        public int? IdMigracion { get; set; }
    }
}
