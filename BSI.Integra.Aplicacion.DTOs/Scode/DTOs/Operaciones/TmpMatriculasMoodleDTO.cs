using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class TmpMatriculasMoodleDTO
	{
		public int Id { get; set; }
		public int IdUsuarioMoodle { get; set; }
		public DateTime FechaInicioMatricula { get; set; }
		public DateTime FechaFinMatricula { get; set; }
		public bool EstadoMatricula { get; set; }
		public int IdCursoMoodle { get; set; }
		public int IdEnRol { get; set; }
		public int IdMatriculaMoodle { get; set; }
	}
}
