using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class FiltroJerarquiaDTO
	{
		public int Id { get; set; }
		public int IdJefe { get; set; }
		public int IdSubordinado { get; set; }
		public string NombresJefe { get; set; }
		public string NombresSubordinado { get; set; }
	}
}
