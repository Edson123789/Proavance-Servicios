using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class AdwordsInsigthDTO
	{
		public string AdsetId { get; set; }
		public int? Age { get; set; }
		public string Country { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateStop { get; set; }
		public string Gender { get; set; }
		public int Impressions { get; set; }
		public string Objective { get; set; }
		public int? Reach { get; set; }
		public double Spend { get; set; }
		public int IdTipo { get; set; }
		public DateTime UltimaActualizacion { get; set; }
		public double? Frequency { get; set; }
		public double FCCosto { get; set; }
		public double FPostLike { get; set; }
		public double FComment { get; set; }
		public int FLinkClick { get; set; }
		public double FLike { get; set; }
		public double FPhotoView { get; set; }
		public double? FPost { get; set; }
		public double? FPageEngagement { get; set; }
		public double? FPostEngagement { get; set; }
		public double? FLeadgenOther { get; set; }
		public string CpcMedio { get; set; }
		public decimal? PosicionMedia { get; set; }
		public DateTime FechaCreacion { get; set; }
	}
}
