using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ScrapingEmpleoClasificacionCertificacionBO : BaseBO
    {
        public int IdScrapingPortalEmpleoResultado { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdCertificacion { get; set; }
        public bool EsAutomatico { get; set; }
        public bool EsValidado { get; set; }
        public bool Obligatorio { get; set; }
    }
}
