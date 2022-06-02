using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PEspecificoSesionFechasDTO
	{
		public int Id { get; set; }
		public int IdPEspecifico { get; set; }
		public DateTime FechaHoraInicio { get; set; }
	}
}
