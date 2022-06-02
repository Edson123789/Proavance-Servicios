using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ClickEnlacePorCampaniaMailchimp
    {
        public List<ClickEnlaceDetallePorCampaniaMailchimp> urls_clicked { get; set; }
        public string campaign_id { get; set; }
        public int total_items { get; set; }
        public List<LinkRelacionadoMailchimp> _links { get; set; }
    }

    public class ClickEnlaceDetallePorCampaniaMailchimp {
        public string id { get; set; }
        public string url { get; set; }
        public int total_clicks { get; set; }
        public decimal click_percentage { get; set; }
        public int unique_clicks { get; set; }
        public decimal unique_click_percentage { get; set; }
        public DateTime? last_click { get; set; }
        public string campaign_id { get; set; }
        public List<LinkRelacionadoMailchimp> _links { get; set; }
    }
}
