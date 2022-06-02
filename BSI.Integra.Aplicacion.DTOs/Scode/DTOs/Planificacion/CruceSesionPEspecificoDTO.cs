using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CruceSesionPEspecificoDTO
	{
		public int Id { get; set; }
		public int IdPEspecifico { get; set; }
		public string Curso { get; set; }
		public string NombreCentroCosto { get; set; }
		public string Ambiente { get; set; }
		public string Expositor { get; set; }
		public string Proveedor { get; set; }
		public double Duracion { get; set; }
		public DateTime FechaHoraInicio { get; set; }
		public DateTime FechaFin { get; set; }
		public int? IdAmbiente { get; set; }
		public int? IdExpositor { get; set; }
		public int? IdProveedor { get; set; }
	}
}
