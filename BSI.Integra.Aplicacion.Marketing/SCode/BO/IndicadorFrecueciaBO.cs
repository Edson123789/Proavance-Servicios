using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class IndicadorFrecuenciaBO : BaseBO
    {
        public int Id { get; set; }
        public int IdIndicadorProblema { get; set; }
        public int IdHora { get; set; }
    }
}
