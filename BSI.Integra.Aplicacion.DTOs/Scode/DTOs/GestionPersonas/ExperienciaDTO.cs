using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ExperienciaDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdAreaTrabajo { get; set; }
		public string AreaTrabajo { get; set; }
	}

	public class ExperienciaFiltroDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdAreaTrabajo { get; set; }
		public string Usuario { get; set; }
	}
}
