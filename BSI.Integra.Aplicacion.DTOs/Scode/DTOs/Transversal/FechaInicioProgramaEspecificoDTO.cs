using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class FechaInicioProgramaEspecificoDTO
	{
		public int IdPEspecifico { get; set; }
		public string NombrePGeneral { get; set; }
		public string NombrePEspecifico { get; set; }
		public string Ciudad { get; set; }
		public string Tipo { get; set; }
		public string TipoCiudad { get; set; }
		public string Duracion { get; set; }
		public string Tiempo { get; set; }
		public string FechaHoraInicio { get; set; }
		public int IdEstadoPEspecifico { get; set; }
		public DateTime FechaCreacion { get; set; }
		public int IdCategoria { get; set; }

	}
}
