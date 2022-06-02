using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ScrapinPortalEmpleoClasificacionAgrupadaDTO
    {
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdAreaTrabajo { get; set; }
        public string AreaTrabajo { get; set; }
        public int? IdAreaFormacion { get; set; }
        public string AreaFormacion { get; set; }
        public int? IdCargo { get; set; }
        public string Cargo { get; set; }
        public int? IdIndustria { get; set; }
        public string Industria { get; set; }

        public string NombreUsuario { get; set; }
    }
}
