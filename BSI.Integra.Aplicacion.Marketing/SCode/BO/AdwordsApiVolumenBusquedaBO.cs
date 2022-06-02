using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class AdwordsApiVolumenBusquedaBO : BaseBO
    {
        public int IdAdwordsApiPalabraClave { get; set; }
        public int PromedioBusqueda { get; set; }
        public int Mes { get; set; }
        public int Anho { get; set; }
        public int IdPais { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
