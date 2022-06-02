using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class FiltroMaterialDTO
	{
		public int[] Area { get; set; }
		public int[] SubArea { get; set; }
		public int[] ProgramaGeneral { get; set; }
		public int[] ProgramaEspecificoPadreIndividual { get; set; }
		public int[] ProgramaEspecificoCurso { get; set; }
		public int[] Grupo { get; set; }
		public int[] EstadoProgramaEspecifico { get; set; }
		public int[] Ciudad { get; set; }
		public int[] Modalidad { get; set; }
		public int[] ListaMaterialEstado{ get; set; }

		public FiltroMaterialDTO() {
			Area = new int[]{ };
			SubArea = new int[]{ };
			ProgramaGeneral = new int[]{ };
			ProgramaEspecificoPadreIndividual = new int[]{ };
			ProgramaEspecificoCurso = new int[]{ };
			Grupo = new int[]{ };
			EstadoProgramaEspecifico = new int[]{ };
			Ciudad = new int[]{ };
			Modalidad = new int[]{ };
			ListaMaterialEstado = new int[]{ };
		}
	}
}
