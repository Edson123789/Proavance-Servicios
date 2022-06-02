using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class FiltroMaterialEntregaDTO
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
		public int[] ListaMaterialEstado { get; set; }

	}
	public class ResultadoMaterialEntregaPEspecificoDetalleDTO
	{
		public int IdPEspecificoPadreIndividual { get; set; }
		public string NombrePEspecificoPadreIndividual { get; set; }
		public int IdPEspecifico { get; set; }
		public string NombrePEspecifico { get; set; }
		public int Grupo { get; set; }
		public int IdMaterialTipo { get; set; }
		public string NombreMaterialTipo { get; set; }
		public int IdMaterialVersion { get; set; }
		public string NombreMaterialVersion { get; set; }
		public int IdMaterialEstado { get; set; }
		public string NombreMaterialEstado { get; set; }
		//public bool EsPrimerMaterial { get; set; }
		//public bool TieneFurAsociado { get; set; }
		//public bool EnviadoAProveedorImpresion { get; set; }
		public ResultadoMaterialEntregaPEspecificoDetalleDTO()
		{
		}
	}
}
