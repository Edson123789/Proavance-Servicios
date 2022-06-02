using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PersonalAreaTrabajoDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Codigo { get; set; }
	}
	public class PersonalAreaTrabajoRegistroDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Codigo { get; set; }
		public string Usuario { get; set; }
	}
}
