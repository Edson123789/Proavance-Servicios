using System;
using System.Collections.Generic;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class EnlaceMailChimpBO : BaseBO
    {
        public string CampaniaMailChimpId { get; set; }
        public string UrlMailChimpId { get; set; }
        public string Url { get; set; }
        public int TotalClicks { get; set; }
        public decimal PorcentajeClicks { get; set; }
        public int ClicksUnicos { get; set; }
        public decimal PorcentajeClicksUnicos { get; set; }
        public DateTime? FechaUltimoClick { get; set; }
        public int? IdMigracion { get; set; }
        public List<InteraccionEnlaceMailchimpBO> ListaInteraccionEnlaceMailChimp { get; set; }
        public EnlaceMailChimpBO() {
            ListaInteraccionEnlaceMailChimp = new List<InteraccionEnlaceMailchimpBO>();
        }
    }
}
