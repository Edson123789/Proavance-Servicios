using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	public class RaPresencialBoletoAereoReservaBO : BaseBO
	{
		public int? IdRaPresencialBoletoAereo { get; set; }
		public int? IdRaAerolinea { get; set; }
		public string LinkBoarding { get; set; }
		public string CodigoReserva { get; set; }
		public int? IdMigracion { get; set; }
	}
}
