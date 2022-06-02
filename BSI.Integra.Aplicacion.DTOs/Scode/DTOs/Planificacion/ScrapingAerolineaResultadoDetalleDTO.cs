using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ScrapingAerolineaResultadoDetalleDTO
	{
		public int Id { get; set; }
		public int IdScrapingAerolineaResultado { get; set; }
		public string NroVuelo { get; set; }
		public string NombreAerolinea { get; set; }
		public int? IdProveedor { get; set; }
		public int IdVueloTipoTramo { get; set; }
		public string VueloTipoTramo { get; set; }
		public int IdCiudadOrigen { get; set; }
		public int IdCiudadDestino { get; set; }
		public bool EsIda { get; set; }
		public DateTime FechaSalida { get; set; }
		public DateTime FechaLlegada { get; set; }
		public string Clase { get; set; }
		public bool AplicaMochila { get; set; }
		public bool AplicaEquipajeMano { get; set; }
		public bool AplicaEquipajeBodega { get; set; }
		public int DuracionMinuto { get; set; }
		public string CodigoCiudadOrigen { get; set; }
		public string CodigoCiudadDestino { get; set; }
	}
}
