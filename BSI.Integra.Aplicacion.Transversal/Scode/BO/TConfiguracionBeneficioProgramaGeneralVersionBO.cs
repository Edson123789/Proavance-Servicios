using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TConfiguracionBeneficioProgramaGeneralVersionBO : BaseBO
    {
        public int IdConfiguracionBeneficioPGneral { get; set; }
        public int? IdVersionPrograma { get; set; }
    }
}
