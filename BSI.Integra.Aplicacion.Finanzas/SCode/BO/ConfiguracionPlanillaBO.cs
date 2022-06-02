using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class ConfiguracionPlanillaBO : BaseBO
    {
        public int IdTipoRemuneracionAdicional { get; set; }
        public string Nombre { get; set; }
        public bool? Recurrente { get; set; }
    }
}
