using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ObtenerDetalleOportunidadDTO
	{
		public string FaseInicio { get; set; }
		public string FaseDestino { get; set; }
		public DateTime? FechaModificacion { get; set; }
		public string Estado { get; set; }
	}
}
