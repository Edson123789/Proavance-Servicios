using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ScrapingConfiguracionDTO
	{
		public int? Id { get; set; }
		public int? IdFur { get; set; }
		public double? MontoProyectadoFur { get; set; }
		public int IdCentroCosto { get; set; }
		public string NombreCentroCosto { get; set; }
		public int? NroGrupoSesion { get; set; }
		public int? NroGrupoCronograma { get; set; }
		public DateTime? FechaHoraInicio { get; set; }
		public DateTime? FechaHoraFin { get; set; }
		public int IdScrapingAerolineaEstadoConsulta { get; set; }
		public string EstadoConsulta { get; set; }
		public int IdCiudadOrigen { get; set; }
		public string CiudadOrigen { get; set; }
		public int IdCiudadDestino { get; set; }
		public string CiudadDestino { get; set; }
		public int IdCiudadAeropuertoOrigen { get; set; }
		public string CiudadAeropuertoOrigen { get; set; }
		public int IdCiudadAeropuertoDestino { get; set; }
		public string CiudadAeropuertoDestino { get; set; }
		public DateTime? FechaIdaProgramada { get; set; }
		public DateTime? FechaRetornoProgramada { get; set; }
		public string TipoVuelo { get; set; }
		public int? NroFrecuencia { get; set; }
		public string TipoFrecuencia { get; set; }
		public decimal? PrecisionIda { get; set; }
		public decimal? PrecisionRetorno { get; set; }
		public bool? TienePasajeComprado { get; set; }
	}
}
