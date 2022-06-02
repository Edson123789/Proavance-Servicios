using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class LeadgenInformacionDTO
    {
        public DateTime created_time { get; set; }
        public string Id { get; set; }
        public string AdsetId { get; set; }
        public string AdId { get; set; }
        public string AdName { get; set; }
        public string AdsetName { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string AreaFormacion { get; set; }
        public string Cargo { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public string InicioCapacitacion { get; set; }
        public string CondicionalPregunta1 { get; set; }
        public string CondicionalPregunta2 { get; set; }
        public bool FormularioMultiple { get; set; }
        public bool FormularioRemarketing { get; set; }
        public string CampaignId { get; set; }
        public string NombreCampania { get; set; }
        public string Name { get; set; }
        public string OptimizationGoal { get; set; }
        public string DailyBudget { get; set; }
        public string BudgetRemaining { get; set; }
        public string EffectiveStatus { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? AdsetCreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public string AdsetStatus { get; set; }
        public int Account { get; set; }
    }
}
