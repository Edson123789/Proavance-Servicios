using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class LlamadaIntegraDTO
    {
        public int? Id { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public string TiempoDuracion { get; set; }
        public string TiempoDuracionMinutos { get; set; }
        public string EstadoLlamada { get; set; }
        public string SubEstadoLlamada { get; set; }
        public string EstadoLlamadaSegunFlow { get; set; }
        public string NombreGrabacion { get; set; }
        public string Webphone { get; set; }


    }
}
