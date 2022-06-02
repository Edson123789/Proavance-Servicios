using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class AprobarRechazarMatriculaMoodleSolicitudDTO
	{
		public string Usuario { get; set; }
		public string TipoPersonal { get; set; }
		public int Habilitado { get; set; }
		public int IdMatriculaMoodleSolicitud { get; set; }
		public int IdMatriculaMoodleSolicitudEstado { get; set; }
		public int? IdMatriculaMoodle { get; set; }
	}
}
