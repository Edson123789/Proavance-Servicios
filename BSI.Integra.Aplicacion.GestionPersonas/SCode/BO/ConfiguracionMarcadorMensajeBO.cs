using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class ConfiguracionMarcadorMensajeBO : BaseBO
    {
        public string Mensaje { get; set; }
        public int? IdMigracion { get; set; }
    }
}
