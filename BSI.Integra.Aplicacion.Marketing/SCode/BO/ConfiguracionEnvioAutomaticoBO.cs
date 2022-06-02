using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ConfiguracionEnvioAutomaticoBO : BaseBO
    {        
        public int? IdEstadoInicial { get; set; }
        public int? IdSubEstadoInicial { get; set; }
        public int? IdEstadoDestino { get; set; }
        public int? IdSubEstadoDestino { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
