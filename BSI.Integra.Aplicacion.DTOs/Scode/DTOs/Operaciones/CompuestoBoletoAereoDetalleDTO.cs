
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CompuestoBoletoAereoDetalleDTO
	{
		public BoletoAereoDetalleDTO BoletoAereoDetalle { get; set; }
		public string Usuario { get; set; }
	}
}