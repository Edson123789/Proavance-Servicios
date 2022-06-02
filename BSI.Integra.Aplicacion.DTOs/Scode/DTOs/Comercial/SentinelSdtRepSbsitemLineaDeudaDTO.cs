using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SentinelSdtRepSbsitemLineaDeudaDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDoc { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Calificacion { get; set; }
        public decimal? MontoDeuda { get; set; }
        public int? DiasVencidos { get; set; }
        public DateTime? FechaReporte { get; set; }
    }
}
