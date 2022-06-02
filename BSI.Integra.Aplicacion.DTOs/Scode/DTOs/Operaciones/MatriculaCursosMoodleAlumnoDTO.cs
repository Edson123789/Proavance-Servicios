using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class MatriculaCursosMoodleAlumnoDTO
	{
		public int IdAlumnoMoodle { get; set; }
		public int IdCursoMoodle { get; set; }
		public int? IdMatriculaMoodle { get; set; }
		public DateTime? FechaInicioMatricula { get; set; }
		public DateTime? FechaFinMatricula { get; set; }
		public string CodigoMatricula { get; set; }
		public int IdAlumnoIntegra { get; set; }
		public int Habilitado { get; set; }
		public string NombreCursoMoodle { get; set; }
		public int? IdMatriculaMoodleSolicitud { get; set; }
		public int? IdMatriculaMoodleSolicitudEstado { get; set; }
		public string MatriculaMoodleSolicitudEstado { get; set; }
	}
}
