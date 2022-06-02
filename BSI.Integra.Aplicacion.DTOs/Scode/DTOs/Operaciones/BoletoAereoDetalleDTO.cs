using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class BoletoAereoDetalleDTO
	{
		public int Id { get; set; }
		public int? IdRaPresencialBoletoAereo { get; set; }
		public int? IdRaAerolinea { get; set; }
		public DateTime? Fecha { get; set; }
		public string Origen { get; set; }
		public DateTime? HoraSalida { get; set; }
		public string Destino { get; set; }
		public DateTime? HoraLlegada { get; set; }
		public string NumeroVuelo { get; set; }
		public string Tipo { get; set; }
		public int? IdRaTipoBoletoAereo { get; set; }
		public string CodigoReserva { get; set; }
		public string Aerolinea { get; set; }
	}
}
