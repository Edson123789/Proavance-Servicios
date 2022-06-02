using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ScrapingEmpleoClasificacionExperienciaDTO
    {
        public int Id { get; set; }
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdCargo { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public int? IdIndustria { get; set; }
        public int? NroAnhos { get; set; }
        public bool? Obligatorio { get; set; }
        public bool EsAutomatico { get; set; }
        public bool EsValidado { get; set; }
    }
}
