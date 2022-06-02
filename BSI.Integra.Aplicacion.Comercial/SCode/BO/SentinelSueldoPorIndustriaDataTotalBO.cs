using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SentinelSueldoPorIndustriaDataTotalBO : BaseBO
    {
        public int? IdCargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }
        public int? Valor { get; set; }
    }
}
