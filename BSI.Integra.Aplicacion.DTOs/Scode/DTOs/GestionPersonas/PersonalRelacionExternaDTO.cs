using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PersonalRelacionExternaDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
		public string PersonalAreaTrabajo { get; set; }
		public string Usuario { get; set; }
	}
}
