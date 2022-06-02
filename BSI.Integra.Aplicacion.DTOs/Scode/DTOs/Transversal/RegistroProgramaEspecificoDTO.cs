using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class RegistroProgramaEspecificoDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Codigo { get; set; }
		public int? IdCentroCosto { get; set; }
		public string EstadoP { get; set; }
		public string Tipo { get; set; }
		public int? IdProgramageneral { get; set; }
		public string Ciudad { get; set; }
		public string UrlDocumentoCronograma { get; set; }
		public bool? CursoIndividual { get; set; }
	}
}
