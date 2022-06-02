using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class HistorialOportunidadDTO
	{
		public int IdOportunidad { get; set; }
		public string CentroCosto { get; set; }
		public string Precio { get; set; }
		public string FaseActual { get; set; }
		public DateTime? FechaCierre { get; set; }
		public string Personal { get; set; }
		public string Descripcion { get; set; }
		public string TipoPago { get; set; }
	}
}
