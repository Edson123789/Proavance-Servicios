using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CronogramaGrupoSesionDTO
	{
		public int IdPespecifico { get; set; }
		public int IdPespecificoSesion { get; set; }
		public int? IdExpositor { get; set; }
		public int IdCiudad { get; set; }
		public DateTime FechaHoraInicio { get; set; }
		public string Ciudad { get; set; }
		public string Duracion { get; set; }
		public string GrupoSesion { get; set; }
		public string GrupoCronograma { get; set; }
		public string NombreExpositor { get; set; }
	}
}
