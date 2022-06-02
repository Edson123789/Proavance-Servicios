using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.DTOs
{
    public class SentinelSdtResVenItemDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public int? CantidadDocs { get; set; }
        public string Fuente { get; set; }
        public string Entidad { get; set; }
        public decimal? Monto { get; set; }
        public short? Cantidad { get; set; }
        public int? DiasVencidos { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
