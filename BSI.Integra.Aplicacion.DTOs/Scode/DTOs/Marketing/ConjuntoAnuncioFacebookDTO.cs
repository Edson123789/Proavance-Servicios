using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 

    public class ConjuntoAnuncioFacebookDTO
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string IdAnuncioFacebook { get; set; }
        public int? AttributionWindowDays { get; set; }
        public int? BidAmount { get; set; }
        public string BillinEevent { get; set; }
        public double? BudgetRemaining { get; set; }
        public string CampaignId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? DailyBudget { get; set; }
        public int? DailyImps { get; set; }
        public string EffectiveStatus { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? IsAutobid { get; set; }
        public bool? IsAveragePricePacing { get; set; }
        public int? LifetimeBudget { get; set; }
        public int? LifetimeImps { get; set; }
        public string Name { get; set; }
        public string OptimizationGoal { get; set; }
        public DateTime? StartTime { get; set; }
        public string Status { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public bool? TieneInsights { get; set; }
        public bool? EsValidado { get; set; }
        public bool? EsIntegra { get; set; }
        public bool? EsPublicado { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public bool? EsRelacionado { get; set; }
        public bool? EsOtros { get; set; }
        public string Total { get; set; }
   

    }


    public class ConjuntoAnuncioFBDTO
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}
