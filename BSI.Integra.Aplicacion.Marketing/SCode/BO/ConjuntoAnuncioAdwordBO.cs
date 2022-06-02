using System;
using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
	public class ConjuntoAnuncioAdwordBO : BaseBO
	{
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

        public Guid? IdMigracion { get; set; }


        private ConjuntoAnuncioAdwordRepositorio _repConjuntoAnuncioAdword;

		public ConjuntoAnuncioAdwordBO()
		{

		}
		public ConjuntoAnuncioAdwordBO(int id)
		{
			_repConjuntoAnuncioAdword = new ConjuntoAnuncioAdwordRepositorio();

			var CAA = _repConjuntoAnuncioAdword.FirstById(id);
			this.Id = CAA.Id;
			this.IdF = CAA.IdF;
			this.CampaignId = CAA.CampaignId;
			this.CreatedTime = CAA.CreatedTime;
			this.EffectiveStatus = CAA.EffectiveStatus;
			this.Name = CAA.Name;
			this.OptimizationGoal = CAA.OptimizationGoal;
			this.StartTime = CAA.StartTime;
			this.Status = CAA.Status;
			this.UpdatedTime = CAA.UpdatedTime;
			this.TieneInsights = CAA.TieneInsights;
			this.EsValidado = CAA.EsValidado;
			this.EsIntegra = CAA.EsIntegra;
			this.EsPublicado = CAA.EsPublicado;
			this.ActivoActualizado = CAA.ActivoActualizado;
			this.FkCampaniaIntegra = CAA.FkCampaniaIntegra;
			this.EsRelacionado = CAA.EsRelacionado;
			this.EsOtros = CAA.EsOtros;
			this.CuentaPublicitaria = CAA.CuentaPublicitaria;
			this.NombreCampania = CAA.NombreCampania;
			this.CentroCosto = CAA.CentroCosto;
			this.TipoCampania = CAA.TipoCampania;
			this.Estado = CAA.Estado;
			this.UsuarioCreacion = CAA.UsuarioCreacion;
			this.UsuarioModificacion = CAA.UsuarioModificacion;
			this.FechaCreacion = CAA.FechaCreacion;
			this.FechaModificacion = CAA.FechaModificacion;
			this.RowVersion = CAA.RowVersion;

	}
	}
}
