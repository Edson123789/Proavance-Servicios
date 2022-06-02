using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AnuncioFacebookMetricaDTO
    {
        public string ad_id { get; set; }
        public string adset_id { get; set; }
        public string campaign_id { get; set; }
        public string account_id { get; set; }
        public string spend { get; set; }
        public string impressions { get; set; }
        public string unique_clicks { get; set; }
        public string clicks { get; set; }
        public string inline_link_clicks { get; set; }
        public string reach { get; set; }
        public string date_start { get; set; }
        public string date_stop { get; set; }
    }

    public class FacebookIntegraIdDTO
    {
        public string IdFacebook { get; set; }
        public string NombreFacebook { get; set; }
        public int IdIntegra { get; set; }
        public int? IdCampaniaFacebook { get; set; }
    }

    public class ConjuntoAnuncioFacebookJerarquiaDTO
    {
        public string id { get; set; }
        public string name { get; set; }
        public string optimization_goal { get; set; }
        public string campaign_id { get; set; }
        public string billing_event { get; set; }
        public string daily_budget { get; set; }
        public string start_time { get; set; }
        public string budget_remaining { get; set; }
        public string status { get; set; }
        public string created_time { get; set; }
        public string effective_status { get; set; }
        public string configured_status { get; set; }
        public string updated_time { get; set; }
    }

    public class AnuncioFacebookMetricaSinProcesarDTO
    {
        public AnuncioFacebookMetricaDTO[] data { get; set; }
        public PaginacionFacebookDTO paging { get; set; }
    }

    public class PaginacionFacebookDTO
    {
        public CursorFacebookDTO cursors { get; set; }
        public string next { get; set; }
        public string after { get; set; }
    }

    public class CursorFacebookDTO
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class ParametroReporteAnuncioFacebookMetricaDTO
    {
        public int IdAreaCapacitacion { get; set; }
    }

    public class AnuncioFacebookMetricaFechaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Usuario { get; set; }
    }

    public class CampaniaFacebookDTO
    {
        public string name { get; set; }
        public string id { get; set; }
        public string account_id { get; set; }
    }

    public class DiccionarioFacebookDTO
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class FacebookRespuestaBatchDTO
    {
        public string code { get; set; }
        public DiccionarioFacebookDTO[] headers { get; set; }
        public string body { get; set; }
    }

    public class CampaniaFacebookSinProcesarDTO
    {
        public CampaniaFacebookDTO[] data { get; set; }
        public PaginacionFacebookDTO paging { get; set; }
    }

    public class AnuncioFacebookDTO
    {
        public string name { get; set; }
        public string adset_id { get; set; }
        public string id { get; set; }
    }

    public class AnuncioFacebookSinProcesarDTO
    {
        public AnuncioFacebookDTO[] data { get; set; }
        public PaginacionFacebookDTO paging { get; set; }
    }

    public class FacebookFormatoPostDTO
    {
        public string method { get; set; }
        public string relative_url { get; set; }
    }

    public class BatchFacebookFormatoPostDTO
    {
        public FacebookFormatoPostDTO[] batch { get; set; }
    }

    public class RespuestaJerarquiaFacebookDTO
    {
        public AnuncioFacebookDTO[] data { get; set; }
        public PaginacionFacebookDTO paging { get; set; }
    }

    public class ReporteAnuncioFacebookMetricaDTO
    {
        public int IdCampaniaFacebook { get; set; }
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string NombreGrupoFiltroProgramaCritico { get; set; }
        public string FacebookNombreCampania { get; set; }
        public int IdFacebookConjuntoAnuncio { get; set; }
        public string FacebookNombreConjuntoAnuncio { get; set; }
        public int IdFacebookAnuncio { get; set; }
        public string FacebookIdAnuncio { get; set; }
        public string FacebookNombreAnuncio { get; set; }
        public double Gasto { get; set; }
        public double PresupuestoDiarioConjuntoAnuncio { get; set; }
        public int Impresiones { get; set; }
        public int CantidadClics { get; set; }
        public DateTime FechaConsulta { get; set; }
        public double ImpresionesPorClic { get; set; }
        public string Moneda { get; set; }
        public int Registros { get; set; }
        public double ClicPorRegistro { get; set; }
        public int RegistrosMuyAlta { get; set; }
        public double PorcentajeRegistrosMuyAlta { get; set; }
        public double ClicsRegistrosMuyAlta { get; set; }
        public double GastoPorRegistrosMuyAlta { get; set; }
    }

    public class ReporteAnuncioFacebookMetricaDiasDTO
    {
        public int IdCampaniaFacebook { get; set; }
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string NombreGrupoFiltroProgramaCritico { get; set; }
        public string FacebookNombreCampania { get; set; }
        public int IdFacebookConjuntoAnuncio { get; set; }
        public string FacebookNombreConjuntoAnuncio { get; set; }
        public double PresupuestoDiarioConjuntoAnuncio { get; set; }
        public int IdFacebookAnuncio { get; set; }
        public string FacebookIdAnuncio { get; set; }
        public string FacebookNombreAnuncio { get; set; }
        public MetricaIndividualDTO Actual { get; set; } = new MetricaIndividualDTO();
        public MetricaIndividualDTO UnDia { get; set; } = new MetricaIndividualDTO();
        public MetricaIndividualDTO TresDias { get; set; } = new MetricaIndividualDTO();
        public MetricaIndividualDTO SieteDias { get; set; } = new MetricaIndividualDTO();
    }

    public class MetricaIndividualDTO
    {
        public double Gasto { get; set; } = 0;
        public int Impresiones { get; set; } = 0;
        public double CostoPorMil { get; set; } = 0;
        public int CantidadClics { get; set; } = 0;
        public DateTime FechaConsulta { get; set; }
        public double ImpresionesPorClic { get; set; } = 0;
        public string Moneda { get; set; } = "US$";
        public int Registros { get; set; } = 0;
        public double ClicPorRegistro { get; set; } = 0;
        public int RegistrosMuyAlta { get; set; } = 0;
        public double PorcentajeRegistrosMuyAlta { get; set; } = 0;
        public double ClicsRegistrosMuyAlta { get; set; } = 0;
        public double GastoPorRegistrosMuyAlta { get; set; } = 0;
    }

    public class AreaAnuncioFacebookMetricaDTO
    {
        public int IdGrupoFiltroProgramaCritico { get; set; }
        public string NombreGrupoFiltroProgramaCritico { get; set; }
    }
}
