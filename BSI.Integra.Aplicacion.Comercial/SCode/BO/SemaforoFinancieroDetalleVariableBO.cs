using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SemaforoFinancieroDetalleVariableBO: BaseBO
    {
        public int IdSemaforoFinancieroDetalle { get; set; }
        public int IdSemaforoFinancieroVariable { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? ValorMaximo { get; set; }
        public int? IdMoneda { get; set; }
        public int? IdMigracion { get; set; }
    }
}
