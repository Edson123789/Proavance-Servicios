using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class MatriculaMoodleSolicitudDTO
	{
		public int Id { get; set; }
		public int IdOportunidad { get; set; }
		public int IdCursoMoodle { get; set; }
		public int IdUsuarioMoodle { get; set; }
		public string CodigoMatricula { get; set; }
		public DateTime FechaInicioMatricula { get; set; }
		public DateTime FechaFinMatricula { get; set; }
		public int IdMatriculaMoodleSolicitudEstado { get; set; }
		public string MatriculaMoodleSolicitudEstado { get; set; }
		public string UsuarioSolicitud { get; set; }
		public DateTime FechaSolicitud { get; set; }
		public string UsuarioAprobacion { get; set; }
		public DateTime? FechaAprobacion { get; set; }
		public string NombreCursoMoodle { get; set; }
		public int Habilitado { get; set; }
	}
}
