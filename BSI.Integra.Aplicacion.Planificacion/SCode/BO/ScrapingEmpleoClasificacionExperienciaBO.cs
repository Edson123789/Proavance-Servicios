using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ScrapingEmpleoClasificacionExperienciaBO : BaseBO
    {
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
