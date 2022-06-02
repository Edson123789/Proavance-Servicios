using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.BO
{
    public class PeriodoMetaBO : BaseBO
    {
        
        public int IdPersonal { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPeriodo { get; set; }
        public int Meta { get; set; }               
        public Guid? IdMigracion { get; set; }
    }
}
