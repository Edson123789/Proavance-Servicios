using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class DatosOportunidadAccesosPortalDTO
	{
		public int IdAlumno { get; set; }
		public int IdPersonalAsignado { get; set; }
		public int IdOportunidad { get; set; }
		public int IdCentroCosto { get; set; }
		public string EmailAsesor { get; set; }
		public string EmailAlumno { get; set; }
		public string CodigoAlumno { get; set; }
		public string Usuario { get; set; }
	}
}
