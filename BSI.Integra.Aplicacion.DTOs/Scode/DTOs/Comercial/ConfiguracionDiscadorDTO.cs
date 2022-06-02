using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConfiguracionDiscadorDTO
    {
        public int Id { get; set; }
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
        public string Usuario { get; set; }


    }
}
