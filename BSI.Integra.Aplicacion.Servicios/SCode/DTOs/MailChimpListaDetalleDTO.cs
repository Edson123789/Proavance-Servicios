using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTOs
{
    public class MailChimpListaDetalleDTO
    {
        public List<Members> members { get; set; }
        public string list_id { get; set; }
        public int total_items { get; set; }
        public List<Link> _links { get; set; }
    }

    public class MailChimpReporteDTO
    {
        public string id { get; set; }
        public string campaign_title { get; set; }
        public string list_id { get; set; }
        public int emails_sent { get; set; }
        public string send_time { get; set; }
    }

    public class MailChimpDetalleCampaniaDTO
    {
        public string id { get; set; }
        public string status { get; set; }
        public string archive_url { get; set; }
        public string long_archive_url { get; set; }
        public int emails_sent { get; set; }
    }

    public class Members
    {
        public string id { get; set; }
        public string email_address { get; set; }
        public string unique_email_id { get; set; }
        public int web_id { get; set; }
        public string email_type { get; set; }
        public string status { get; set; }
        public MergeFields merge_fields { get; set; }
        public Stats stats { get; set; }
        public DateTime? timestamp_signup { get; set; }
        public string ip_opt { get; set; }
        public DateTime? timestamp_opt { get; set; }
        public int member_rating { get; set; }
        public DateTime? last_changed { get; set; }
        public string language { get; set; }
        public bool vip { get; set; }
        public string email_client { get; set; }
        public Location location { get; set; }
        public string source { get; set; }
        public int tags_count { get; set; }
        public string list_id { get; set; }
        public List<Link> _links { get; set; }
    }
    public class MergeFields
    {
        public string FNAME { get; set; }
        public string LNAME { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE { get; set; }
        public string IDPMLC { get; set; }
    }
    public class Stats
    {
        public int avg_open_rate { get; set; }
        public int avg_click_rate { get; set; }
    }
    public class Location
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string gmtoff { get; set; }
        public string dstoff { get; set; }
        public string country_code { get; set; }
        public string timezone { get; set; }
    }

    public class Link
    {
        public string rel { get; set; }
        public string href { get; set; }
        public string method { get; set; }
        public string targetSchema { get; set; }
    }

}

