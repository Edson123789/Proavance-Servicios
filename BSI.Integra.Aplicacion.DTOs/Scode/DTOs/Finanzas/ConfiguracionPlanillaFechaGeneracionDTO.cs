using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfiguracionPlanillaFechaGeneracionDTO
    {
        public int IdConfiguracionPlanilla { get; set; }
        public bool EsMensual { get; set; }
        public DateTime FechaProceso { get; set; }
        public int IdTipoRemuneracion { get; set; }
        public string NombreRemuneracion { get; set; }
        public bool Estado { get; set; }
    }
}
