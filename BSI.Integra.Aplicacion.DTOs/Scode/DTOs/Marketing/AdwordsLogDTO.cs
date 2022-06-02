using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class AdwordsLogDTO
	{
		public int Id { get; set; }
		public string Mensaje { get; set; }
		public string Usuario { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime FechaModificacion { get; set; }
	}
}
