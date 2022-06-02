using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ScrapingEmpleoClasificacionEstudioDTO
    {
        public int Id { get; set; }
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdTipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public bool EsAutomatico { get; set; }
        public bool EsValidado { get; set; }
    }
}
