using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ScrapingAerolineaResultadoDetalleEscalaDTO
	{
		public int Id { get; set; }
		public int IdScrapingAerolineaResultado { get; set; }
		public string NroVuelo { get; set; }
		public string NombreAerolinea { get; set; }
		public int? IdProveedor { get; set; }
		public string NombreCiudadOrigen { get; set; }
		public string NombreCiudadDestino { get; set; }
		public DateTime FechaSalida { get; set; }
		public DateTime FechaLlegada { get; set; }
		public string Clase { get; set; }
		public int DuracionMinuto { get; set; }
	}
}
