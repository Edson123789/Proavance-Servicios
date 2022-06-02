using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class BoletoAereoFiltroDTO
	{
		public int Id { get; set; }
		public int IdRaSesion { get; set; }
		public int IdRaHotel { get; set; }
		public int IdRaMovilidad { get; set; }
		public DateTime? FechaCoordinacionEstadia { get; set; }
		public string NombreHotel { get; set; }
		public string NombreMovilidad { get; set; }
		public string TipoMovilidad { get; set; }
	}
}
