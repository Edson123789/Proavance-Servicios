using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class RealizarSolicitudMatriculaMoodleDTO
	{
		public int IdUsuarioMoodle { get; set; }
		public int IdCursoMoodle { get; set; }
		public string CodigoMatricula { get; set; }
		public int IdOportunidad { get; set; }
		public DateTime FechaInicioMatricula { get; set; }
		public DateTime FechaFinMatricula { get; set; }
		public string Usuario { get; set; }
		public string TipoPersonal { get; set; }
		public int Habilitado { get; set; }
		public int? IdMatriculaMoodleSolicitud { get; set; }
		public int IdMatriculaMoodleSolicitudEstado { get; set; }
		public int? IdMatriculaMoodle { get; set; }
		public bool? Flag { get; set; }

	}
}
