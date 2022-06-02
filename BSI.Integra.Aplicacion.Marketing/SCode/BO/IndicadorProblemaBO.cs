using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class IndicadorProblemaBO : BaseBO
    {
        public int Id { get; set; }
        public int IdProblema { get; set; }
        public int IdIndicador { get; set; }
        public int IdOperadorComparacion { get; set; }
        public decimal Valor { get; set; }
        public int MuestraMinima { get; set; }
    }
}
