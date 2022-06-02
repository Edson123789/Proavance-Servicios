using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SentinelSueldoPorIndustriaBO : BaseBO
    {
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public int? Tipo { get; set; }
    }
}
