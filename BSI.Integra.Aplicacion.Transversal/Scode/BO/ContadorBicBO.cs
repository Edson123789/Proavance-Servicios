using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ContadorBicBO : BaseBO
    {
        public int IdOportunidad { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
        public int? IdMigracion { get; set; }
    }
}
