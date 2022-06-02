using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class ConfiguracionPlanillaFechasBO : BaseBO
    {
        public int IdConfiguracionPlanilla { get; set; }
        public DateTime FechaProceso { get; set; }
        public bool? CalculoReal { get; set; }
        public DateTime? FechaProcesoReal { get; set; }
    }
}
