using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadEjecutadaTrescxDTO
    {
        public int DuracionTimbrado { get; set; }
        public int DuracionContesto { get; set; }
        public string EstadoLlamada { get; set; }
        public DateTime FechaLlamada { get; set; }
    }
}
