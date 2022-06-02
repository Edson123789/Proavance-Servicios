using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class AdwordsApiPalabraClaveBO : BaseBO
    {
        public string PalabraClave { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
