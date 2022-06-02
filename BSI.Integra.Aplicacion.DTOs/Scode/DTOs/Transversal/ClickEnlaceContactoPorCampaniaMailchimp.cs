using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ClickEnlaceContactoPorCampaniaMailchimp
    {
        public List<ClickEnlaceDetalleContactoPorCampaniaMailchimp> members { get; set; }
        public string campaign_id { get; set; }
        public int total_items { get; set; }
        public List<LinkRelacionadoMailchimp> _links { get; set; }
    }

    public class ClickEnlaceDetalleContactoPorCampaniaMailchimp
    {
        public string email_id { get; set; }
        public string email_address { get; set; }
        public CamposFusionados merge_fields { get; set; }
        public bool vip { get; set; }
        public int clicks { get; set; }
        public string campaign_id { get; set; }
        public string url_id { get; set; }
        public string list_id { get; set; }
        public bool list_is_active { get; set; }
        public string contact_status { get; set; }
        public List<LinkRelacionadoMailchimp> _links { get; set; }
    }
}
