using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	public class RaPresencialBoletoAereoBO : BaseBO
	{
		public int? IdRaSesion { get; set; }
		public int? IdRaHotel { get; set; }
		public int? IdRaMovilidad { get; set; }
		public DateTime? FechaCoordinacionEstadia { get; set; }
		public int? IdMigracion { get; set; }
	}
}
