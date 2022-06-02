using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfiguracionPlanillaFechasDTO
    {
        public int Id { get; set; }
        public int IdConfiguracionPlanilla { get; set; }
        public DateTime FechaProceso { get; set; }
        public bool? CalculoReal { get; set; }
        public DateTime? FechaProcesoReal { get; set; }
        public string Usuario { get; set; }
    }
}
