using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class LlamadaRegularizacionDTO
    {
        public int Id { get; set; }
        public DateTime FechaFinLlamada { get; set; }
        public DateTime FechaInicioLlamada { get; set; }
        public string Anexo { get; set; }
        public string TelefonoDestino { get; set; }
        public int TiempoTimbrado { get; set; }
        public int TiempoContesto { get; set; }
    }
}
