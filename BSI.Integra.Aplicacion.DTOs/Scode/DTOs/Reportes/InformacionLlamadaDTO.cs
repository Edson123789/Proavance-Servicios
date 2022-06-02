using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InformacionLlamadaDTO
    {
        public int? Id { get; set; }
        public int? DuracionTimbrado { get; set; }
        public int? DuracionContesto { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string EstadoLlamada { get; set; }
        public string SubEstadoLlamada { get; set; }
        public string NombreGrabacion { get; set; }
        public string MinutosPerdidos { get; set; }
        public string Webphone { get; set; }

    }
}
