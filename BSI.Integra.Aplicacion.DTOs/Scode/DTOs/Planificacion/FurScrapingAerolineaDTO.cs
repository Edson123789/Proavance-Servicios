using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class FurScrapingAerolineaDTO
	{
		public int IdScrapingAerolineaResultado { get; set; }
		public int IdScrapingAerolineaConfiguracion { get; set; }
		public decimal Precio { get; set; }
		public int? IdFur { get; set; }
		public int IdCentroCosto { get; set; }
		public DateTime Fecha { get; set; }
	}
}
