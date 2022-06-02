using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TConjuntoAnuncioAdword
    {
        public int Id { get; set; }
        public string IdF { get; set; }
        public string CampaignId { get; set; }
        public DateTime? CreatedTime { get; set; }
        public string EffectiveStatus { get; set; }
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
        public int? FkCampaniaIntegra { get; set; }
        public bool? EsRelacionado { get; set; }
        public bool? EsOtros { get; set; }
        public int? CuentaPublicitaria { get; set; }
        public string NombreCampania { get; set; }
        public string CentroCosto { get; set; }
        public int? TipoCampania { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
