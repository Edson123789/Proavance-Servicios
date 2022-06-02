using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class ConjuntoAnuncioFacebookBO : BaseBO
    {
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
        public bool? ActivoActualizado { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public bool? EsRelacionado { get; set; }
        public bool? EsOtros { get; set; }
        public int? CuentaPublicitaria { get; set; }
        public string NombreCampania { get; set; }
        public string CentroCosto { get; set; }
        public int? IdCampaniaFacebook { get; set; }
        public string ConfiguredStatus { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
