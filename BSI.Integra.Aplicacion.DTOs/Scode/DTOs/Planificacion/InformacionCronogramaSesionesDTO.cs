using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class InformacionCronogramaSesionesDTO
	{
		public int Id { get; set; }
		public DateTime FechaHoraInicio { get; set; }
		public int? IdExpositor { get; set; }
		public int? IdProveedor { get; set; }
		public int? IdAmbiente { get; set; }
		public int? IdModalidadCurso { get; set; }
		public string Usuario { get; set; }
		public int? GrupoSesion { get; set; }
		public bool AplicarCambios { get; set; }
		public bool? MostrarPortalWeb { get; set; }
	}
}
