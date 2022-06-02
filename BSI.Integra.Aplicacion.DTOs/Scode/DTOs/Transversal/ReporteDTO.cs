using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ReporteDTO
	{
		public string AdGroupId { get; set; }
		public string AdGroup { get; set; }
		public string CampaignId { get; set; }
		public string Campaign { get; set; }
		public string AdGroupState { get; set; }
		public string Network { get; set; }
		public string Day { get; set; }
		public long Cost { get; set; }
		public double CTR { get; set; }
		public long AvgCPC { get; set; }
		public double AvgPosition { get; set; }
		public long Clicks { get; set; }
		public int Impressions { get; set; }
	}
}
