using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class SkypeReunionDTO
	{
		public int Id { get; set; }
		public int IdRaCentroCosto { get; set; }
		public string ReunionId { get; set; }
		public string UrlBase { get; set; }
		public bool Activo { get; set; }
		public DateTime FechaModificacion { get; set; }
	}
}
