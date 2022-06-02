using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ScrapingEmpleoClasificacionCertificacionDTO
    {
        public int Id { get; set; }
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdCertificacion { get; set; }
        public bool EsAutomatico { get; set; }
        public bool EsValidado { get; set; }
        public bool Obligatorio { get; set; }
    }
}
