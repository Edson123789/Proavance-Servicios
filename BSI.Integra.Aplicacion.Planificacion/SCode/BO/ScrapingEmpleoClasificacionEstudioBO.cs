using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ScrapingEmpleoClasificacionEstudioBO : BaseBO
    {
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdTipoEstudio { get; set; }
        public int? IdAreaFormacion { get; set; }
        public bool EsAutomatico { get; set; }
        public bool EsValidado { get; set; }
    }
}
