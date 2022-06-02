using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoActividadEjecutadaDetalleDTO
    {
        public string DuracionTimbrado { get; set; }
        public string DuracionContesto { get; set; }
        public string EstadoLlamada { get; set; }
        public DateTime? FechaLlamada { get; set; }
        public string EstadoClasificacion { get; set; }
    }
}
