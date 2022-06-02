using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteIndicadoresCampaniasMailingDTO
    {
    }

    public class PrioridadMailChimpCampaniaListaDTO
    {
        public int Id { get; set; }
        public string CampaniaMailChimpId { get; set; }
        public string ListaMailChimpId { get; set; }
        public int? CantidadAperturaUnica { get; set; }
        public int CantidadCorreoAbierto { get; set; }
        public DateTime FechaConsulta { get; set; }
    }

    public class InformacionCompletaMailchimpIntervaloFechaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Usuario { get; set; }
    }
    public class ReporteIndicadoresMailingFechaDTO
    {
        public DateTime FechaConsulta { get; set; }
        public bool MapeoCompleto { get; set; }
        public string Usuario { get; set; }
    }

    public class ReporteCampaniaMailchimpResultadoCrudoDTO
    {
        public string id { get; set; }
        public string campaign_title { get; set; }
        public string list_id { get; set; }
        public string list_name { get; set; }
        public int emails_sent { get; set; }
        public int abuse_reports { get; set; }
        public int unsubscribed { get; set; }
        public string send_time { get; set; }
        public ReboteCrudoMailChimpResultadoDTO bounces { get; set; }
        public AbiertoCrudoMailChimpResultadoDTO opens { get; set; }
        public ClicCrudoMailChimpResultadoDTO clicks { get; set; }
    }

    public class ReboteCrudoMailChimpResultadoDTO
    {
        public int hard_bounces { get; set; }
        public int soft_bounces { get; set; }
        public int syntax_errors { get; set; }
    }

    public class AbiertoCrudoMailChimpResultadoDTO
    {
        public int opens_total { get; set; }
        public int unique_opens { get; set; }
        public string open_rate { get; set; }
        public string last_open { get; set; }
    }

    public class ClicCrudoMailChimpResultadoDTO
    {
        public int clicks_total { get; set; }
        public int unique_clicks { get; set; }
        public int unique_subscriber_clicks { get; set; }
        public string click_rate { get; set; }
        public string last_click { get; set; }
    }

    #region InformacionCampaniaMailchimpFormatoDTO
    public class InformacionCampaniaMailchimpFormatoDTO
    {
        public List<InformacionCampaniaMailchimpCrudoDTO> campaigns { get; set; }
        public int total_items { get; set; }
    }

    public class InformacionCampaniaMailchimpCrudoDTO
    {
        public string id { get; set; }
        public int web_id { get; set; }
        public string type { get; set; }
        public string create_time { get; set; }
        public string status { get; set; }
        public int emails_sent { get; set; }
        public string send_time { get; set; }
        public string content_type { get; set; }
        public InformacionCampaniaMailchimpRecipienteCrudoDTO recipients { get; set; }
        public InformacionCampaniaMailchimpConfiguracionCrudoDTO settings { get; set; }
        public InformacionCampaniaMailchimpResumenCrudoDTO report_summary { get; set; }
    }

    public class InformacionCampaniaMailchimpRecipienteCrudoDTO
    {
        public string list_id { get; set; }
        public string list_name { get; set; }
        public int recipient_count { get; set; }
    }

    public class InformacionCampaniaMailchimpConfiguracionCrudoDTO
    {
        public string subject_line { get; set; }
        public string title { get; set; }
        public string from_name { get; set; }
        public string reply_to { get; set; }
    }
    public class InformacionCampaniaMailchimpResumenCrudoDTO
    {
        public int? opens { get; set; }
        public int? unique_opens { get; set; }
        public string open_rate { get; set; }
        public int? clicks { get; set; }
        public int? subscriber_clicks { get; set; }
        public string click_rate { get; set; }
    }
    #endregion

    #region InformacionListaMailchimpFormatoDTO
    public class InformacionListaMailchimpFormatoDTO
    {
        public List<InformacionListaMailchimpCrudoDTO> lists { get; set; }
        public int total_items { get; set; }
    }

    public class InformacionListaMailchimpCrudoDTO
    {
        public string id { get; set; }
        public int web_id { get; set; }
        public string name { get; set; }
        public InformacionListaMailchimpContactoCrudoDTO contact { get; set; }
        public InformacionListaMailchimpCampaniaDefectoCrudoDTO campaign_defaults { get; set; }
        public string date_created { get; set; }
        public int list_rating { get; set; }
        public InformacionListaMailchimpEstadisticaCrudoDTO stats { get; set; }
    }

    public class InformacionListaMailchimpContactoCrudoDTO
    {
        public string company { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
    }

    public class InformacionListaMailchimpCampaniaDefectoCrudoDTO
    {
        public string from_name { get; set; }
        public string from_email { get; set; }
        public string subject { get; set; }
        public string language { get; set; }
    }

    public class InformacionListaMailchimpEstadisticaCrudoDTO
    {
        public int member_count { get; set; }
        public int unsubscribe_count { get; set; }
        public int cleaned_count { get; set; }
        public int member_count_since_send { get; set; }
        public int unsubscribe_count_since_send { get; set; }
        public int cleaned_count_since_send { get; set; }
        public int campaign_count { get; set; }
        public string campaign_last_sent { get; set; }
        public int merge_field_count { get; set; }
        public string avg_sub_rate { get; set; }
        public string avg_unsub_rate { get; set; }
        public string target_sub_rate { get; set; }
        public string open_rate { get; set; }
        public string click_rate { get; set; }
        public string last_sub_date { get; set; }
        public string last_unsub_date { get; set; }
    }
    #endregion

    #region InformacionMiembroMailchimpFormatoDTO
    public class InformacionMiembroMailchimpFormatoDTO
    {
        public List<InformacionMiembroMailchimpCrudoDTO> members { get; set; }
        public int total_items { get; set; }
    }

    public class InformacionMiembroMailchimpCrudoDTO
    {
        public string id { get; set; }
        public string email_address { get; set; }
        public string unique_email_id { get; set; }
        public string contact_id { get; set; }
        public string full_name { get; set; }
        public int web_id { get; set; }
        public string email_type { get; set; }
        public string status { get; set; }
        public InformacionMiembroMailchimpEstadisticaCrudoDTO stats { get; set; }
        public string timestamp_opt { get; set; }
        public int member_rating { get; set; }
        public string last_changed { get; set; }
        public string email_client { get; set; }
        public string source { get; set; }
        public string list_id { get; set; }
    }

    public class InformacionMiembroMailchimpEstadisticaCrudoDTO
    {
        public string avg_open_rate { get; set; }
        public string avg_click_rate { get; set; }
    }
    #endregion

    public class ListaReporteCampaniaMailchimpResultadoCrudoDTO
    {
        public List<ReporteCampaniaMailchimpResultadoCrudoDTO> reports { get; set; }
        public int total_items { get; set; }
    }

    public class DetalleAperturaMiembrosMailchimpResultadoCrudoDTO
    {
        public string campaign_id { get; set; }
        public string list_id { get; set; }
        public string email_id { get; set; }
        public int opens_count { get; set; }
        public List<FechaAperturaCorreoMailChimpReporteDTO> opens { get; set; }
    }

    public class FechaAperturaCorreoMailChimpReporteDTO
    {
        public string timestamp { get; set; }
    }

    public class ListaActividadDiariaMailChimpCrudoDTO
    {
        public List<DetalleAperturaMiembrosMailchimpResultadoCrudoDTO> members { get; set; }
        public string campaign_id { get; set; }
        public int total_opens { get; set; }
        public int total_items { get; set; }
    }

    public class MailChimpParametroFormatoDTO
    {
        public int count { get; set; }
        public int offset { get; set; }
    }
}
