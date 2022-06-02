using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.DTOs
{
    public class SentinelRepLegItemDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RazonSocial { get; set; }
        public string Cargo { get; set; }
        public string SemaforoActual { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
