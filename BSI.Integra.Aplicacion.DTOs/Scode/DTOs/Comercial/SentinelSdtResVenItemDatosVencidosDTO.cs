using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SentinelSdtResVenItemDatosVencidosDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public int? CantidadDocs { get; set; }
        public string Fuente { get; set; }
        public decimal? Monto { get; set; }
        public Int32? Cantidad { get; set; }
        public int? DiasVencidos { get; set; }
        public string Entidad { get; set; }
    }
}
