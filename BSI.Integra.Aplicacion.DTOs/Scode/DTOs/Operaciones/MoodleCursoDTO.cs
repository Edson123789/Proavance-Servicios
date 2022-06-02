using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class MoodleCursoDTO
	{
		public int Id { get; set; }
		public int IdCategoria { get; set; }
		public int IdCursoMoodle { get; set; }
		public string NombreCategoria { get; set; }
		public string NombreCursoMoodle { get; set; }
	}
}
