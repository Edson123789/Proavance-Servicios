using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class BoletoAereoReservaFiltroDTO
	{
		public int Id { get; set; }
		public int? IdRaPresencialBoletoAereo { get; set; }
		public int? IdRaAerolinea { get; set; }
		public string LinkBoarding { get; set; }
		public string CodigoReserva { get; set; }
		public string NombreAerolinea { get; set; }
	}
}
