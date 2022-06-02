using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class RegistroOportunidadAlumnoDTO
	{
		public AlumnoFormularioOportunidadDTO Alumno { get; set; }
		public OportunidadFormularioDTO Oportunidad { get; set; }
		public string Usuario { get; set; }
	}
}
