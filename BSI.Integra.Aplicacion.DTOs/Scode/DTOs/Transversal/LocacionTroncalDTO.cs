using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class LocacionTroncalDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdCiudad { get; set; }
		public int? CodigoBS { get; set; }
		public string DenominacionBS { get; set; }

	}
}
