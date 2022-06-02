using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ScrapingAerolineaConfiguracionRegistroDTO
	{
		public int Id { get; set; }
		public int? IdCentroCosto { get; set; }
		public int IdCiudadOrigen { get; set; }
		public int IdCiudadDestino { get; set; }
		public DateTime FechaIda { get; set; }
		public DateTime FechaRetorno { get; set; }
		public int NroFrecuencia { get; set; }
		public string TipoFrecuencia { get; set; }
		public string TipoVuelo { get; set; }
	}
}
