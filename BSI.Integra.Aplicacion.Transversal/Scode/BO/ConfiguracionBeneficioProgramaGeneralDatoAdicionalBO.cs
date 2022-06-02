using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.BO
{
    public class ConfiguracionBeneficioProgramaGeneralDatoAdicionalBO : BaseBO
    {
        public int IdConfiguracionBeneficioPgeneral { get; set; }
        public int IdBeneficioDatoAdicional { get; set; }
    }
}
