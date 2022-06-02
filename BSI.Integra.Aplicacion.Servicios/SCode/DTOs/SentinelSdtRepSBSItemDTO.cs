using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.DTOs
{
    public class SentinelSdtRepSBSItemDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDoc { get; set; }
        public string NroDoc { get; set; }
        public string NombreRazonSocial { get; set; }
        public string Calificacion { get; set; }
        public decimal? MontoDeuda { get; set; }
        public int? DiasVencidos { get; set; }
        public DateTime? FechaReporte { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
