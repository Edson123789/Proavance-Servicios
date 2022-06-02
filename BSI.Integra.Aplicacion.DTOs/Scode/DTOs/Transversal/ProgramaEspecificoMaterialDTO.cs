using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ProgramaEspecificoMaterialDTO
	{
		public int IdProgramaEspecifico { get; set; }
		public string ProgramaEspecifico { get; set; }
		public string EstadoProgramaEspecifico { get; set; }
		public string Modalidad { get; set; }
		public string Ciudad { get; set; }
	}
	public class ProgramaEspecificoMaterialFiltroDTO
	{
		public string IdProgramaEspecifico { get; set; }
		public string IdCentroCosto { get; set; }
		public string CodigoBs { get; set; }
		public string IdEstadoPEspecifico { get; set; }
		public string IdModalidadCurso { get; set; }
		public string IdPGeneral { get; set; }
		public string IdArea { get; set; }
		public string IdSubArea { get; set; }
		public int? IdCentroCostoD { get; set; }
	}
}
