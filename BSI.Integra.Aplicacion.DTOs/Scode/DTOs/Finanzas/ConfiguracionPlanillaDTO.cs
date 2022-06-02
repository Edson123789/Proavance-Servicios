using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoConfiguracionPlanillaDTO
    {
        public ConfiguracionPlanillaDTO ConfiguracionPlanilla { get; set; }
        public List<ConfiguracionPlanillaFechasDTO> ConfiguracionPlanillaFechas { get; set; }
    }

    public class ConfiguracionPlanillaDTO
    {
        public int Id { get; set; }
        public int IdTipoRemuneracionAdicional { get; set; }
        public string Nombre { get; set; }
        public bool? Recurrente { get; set; }
        public string Usuario { get; set; }
    }
}
