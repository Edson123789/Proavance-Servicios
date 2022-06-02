using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	public class RaPresencialBoletoAereoDetalleBO : BaseBO
	{
		public int? IdRaPresencialBoletoAereo { get; set; }
		public int? IdRaAerolinea { get; set; }
		public DateTime? Fecha { get; set; }
		public string Origen { get; set; }
		public DateTime? HoraSalida { get; set; }
		public string Destino { get; set; }
		public DateTime? HoraLlegada { get; set; }
		public string NumeroVuelo { get; set; }
		public string CodigoReserva { get; set; }
		public int? IdRaAgencia { get; set; }
		public double? PagoReal { get; set; }
		public bool? PagoAerolinea { get; set; }
		public int? IdFur { get; set; }
		public bool? Pagado { get; set; }
		public DateTime? FechaCompraPasaje { get; set; }
		public DateTime? FechaVencimientoReserva { get; set; }
		public int? IdRaTipoBoletoAereo { get; set; }
	}
}
