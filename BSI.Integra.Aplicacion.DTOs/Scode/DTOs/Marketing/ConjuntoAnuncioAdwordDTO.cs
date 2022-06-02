using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ConjuntoAnuncioAdwordDTO
	{
		public int Id { get; set; }
		public string id_f { get; set; }
		public string campaign_id { get; set; }
		public DateTime? created_time { get; set; }
		public string effective_status { get; set; }
		public string name { get; set; }
		public string optimization_goal { get; set; }
		public DateTime? start_time { get; set; }
		public string status { get; set; }
		public DateTime? updated_time { get; set; }
		public bool? tiene_insights { get; set; }
		public bool? es_validado { get; set; }
		public bool? es_integra { get; set; }
		public bool? es_publicado { get; set; }
		public bool? activo_actualizado { get; set; }
		public int? FK_CampaniaIntegra { get; set; }
		public bool? es_relacionado { get; set; }
		public bool? es_otros { get; set; }
		public int? CuentaPublicitaria { get; set; }
		public string NombreCampania { get; set; }
		public string CentroCosto { get; set; }
		public int? tipo_campania { get; set; }
		public DateTime FechaCreacion { get; set; }
	}
}
