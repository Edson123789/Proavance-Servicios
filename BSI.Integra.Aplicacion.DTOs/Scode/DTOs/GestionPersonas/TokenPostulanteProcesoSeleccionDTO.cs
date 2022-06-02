using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class TokenPostulanteProcesoSeleccionDTO
	{
		public int Id { get; set; }
		public int IdPostulanteProcesoSeleccion { get; set; }
		public string Token { get; set; }
		public string TokenHash { get; set; }
		public bool Activo { get; set; }
	}
}
