using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class ReferidoConfiguracionBO : BaseBO
    {
        public int IdOrigen { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
    }
}
