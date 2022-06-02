using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CentroCostoHijoDTO
	{
		public int IdCentroCosto { get; set; }
		public int PEspecificoPadreId { get; set; }
		public int IdCentroCostoHijo { get; set; }
		public int PEspecificoHijoId { get; set; }
	}
}
