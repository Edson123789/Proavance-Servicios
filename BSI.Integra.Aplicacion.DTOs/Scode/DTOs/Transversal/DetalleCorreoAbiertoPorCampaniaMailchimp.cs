using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CorreoAbiertoPorCampaniaMailchimp {
        public List<DetalleCorreoAbiertoPorCampaniaMailchimp> members { get; set; }
        public string campaign_id { get; set; }
        public int total_opens { get; set; }
        public int total_items { get; set; }
        public List<LinkRelacionadoMailchimp> _links { get; set; }
    }

    public class DetalleCorreoAbiertoPorCampaniaMailchimp
    {
        public string campaign_id { get; set; }
        public string list_id { get; set; }
        public bool list_is_active { get; set; }
        public string contact_status { get; set; }
        public string email_id { get; set; }
        public string email_address { get; set; }
        public CamposFusionados merge_fields { get; set; }
        public bool vip { get; set; }
        //public string opens_count { get; set; }
        public int opens_count { get; set; }
        public List<CorreosAbiertos> opens { get; set; }
    }

    public class CorreosAbiertos {
        public DateTime timestamp { get; set; }
    }

    public class CamposFusionados
    {
        public string FNAME { get; set; }
        public string LNAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public string IDPMLC { get; set; }
    }

    public class LinkRelacionadoMailchimp
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string method { get; set; }
        public string targetSchema { get; set; }
    }
}
