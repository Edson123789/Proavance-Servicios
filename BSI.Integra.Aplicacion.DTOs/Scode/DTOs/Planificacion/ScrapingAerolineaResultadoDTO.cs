using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ScrapingAerolineaResultadoDTO
	{
		public int Id { get; set; }
		public int IdScrapingAerolineaConfiguracion { get; set; }
		public decimal Precio { get; set; }
		public int IdScrapingPagina { get; set; }
		public int? IdPespecifico { get; set; }
		public int IdCentroCosto { get; set; }
		public int? NroSesionGrupo { get; set; }
		public int? NroGrupoCronograma { get; set; }
		public int IdCiudadOrigen { get; set; }
		public int IdCiudadDestino { get; set; }
		public bool EsActual { get; set; }
		public DateTime FechaIda { get; set; }
		public DateTime FechaRetorno { get; set; }
		public string CodigoCiudadOrigen { get; set; }
		public string CodigoCiudadDestino { get; set; }
	}
}
