using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class ConfiguracionDiscadorBO : BaseBO
    {
        public int IdEstadoOcurrencia { get; set; }
        public bool ContestaLlamada { get; set; }
        public int IdOperadorComparacionTimbradoSegundos { get; set; }
        public int TiempoTimbrado { get; set; }
        public int IdOperadorComparacionEfectivoSegundos { get; set; }
        public int TiempoEfectivo { get; set; }
        public int CantidadIntentosContacto { get; set; }
        public int TiempoEsperaLlamadaSegundos { get; set; }
        public bool DesvioLlamada { get; set; }
        public bool BuzonVoz { get; set; }
        public bool NoConectaLlamada { get; set; }
        public bool TelefonoApagado { get; set; }
        public bool NumeroNoExiste { get; set; }
        public bool NumeroSuspendido { get; set; }

        public ConfiguracionDiscadorBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
        }
    }
}