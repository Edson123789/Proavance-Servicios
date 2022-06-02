using System;
using BSI.Integra.Aplicacion.Base.BO;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class ConfiguracionEnvioMailingDetalleBO : BaseBO
    {
        public int IdConfiguracionEnvioMailing { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public string Asunto { get; set; }
        public string CuerpoHtml { get; set; }
        public bool EnviadoCorrectamente { get; set; }
        public string MensajeError { get; set; }
        public int? IdMigracion { get; set; }
        public int IdMandrilEnvioCorreo { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdOportunidad { get; set; }
    }
}
