using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class MaterialPEspecificoEntregaAlumnoDTO
	{
		public int IdAlumno { get; set; }
		public string Alumno { get; set; }
		public int IdMatriculaCabecera { get; set; }
		public int IdMaterialPEspecificoDetalle { get; set; }
		public int IdMaterialTipo { get; set; }
		public string NombreMaterialTipo { get; set; }
		public int? IdEstadoEntregaMaterialAlumno { get; set; }
		public string EstadoEntregaMaterialAlumno { get; set; }
		public int Grupo { get; set; }
		public int IdPEspecifico { get; set; }
		public string NombrePEspecifico { get; set; }
	}

	public class MaterialAlumnoAgrupadoDTO
	{
		public int IdAlumno { get; set; }
		public string Alumno { get; set; }
		public int IdMatriculaCabecera { get; set; }
		public int Grupo { get; set; }
		public int IdPEspecifico { get; set; }
		public string NombrePEspecifico { get; set; }
		public List<MaterialAlumnoAgrupadoDetalleDTO> Materiales { get; set; }
	}
	public class MaterialAlumnoAgrupadoDetalleDTO
	{
		public int IdMaterialPEspecificoDetalle { get; set; }
		public int IdMaterialTipo { get; set; }
		public string MaterialTipo { get; set; }
		public int? IdEstadoEntregaMaterialAlumno { get; set; }
		public string EstadoEntregaMaterialAlumno { get; set; }
	}

	public class MaterialTipoColumnaDTO
	{
		public int IdMaterialTipo { get; set; }
		public string MaterialTipo { get; set; }
		public int IdMaterialPEspecificoDetalle { get; set; }
	}

	public class MaterialAlumnoEntregaRegistroDTO
	{
		public int IdMatriculaCabecera { get; set; }
		public Dictionary<int, int> ClaveValor { get; set; }
		public string Usuario { get; set; }
	}
	public class RegistroEntregaMaterialTipoDTO
	{
		public int Id { get; set; }
		public int IdMatriculaCabecera { get; set; }
		public int IdMaterialPEspecificoDetalle { get; set; }
		public int IdEstadoEntregaMaterialAlumno { get; set; }
		public string Usuario { get;set; }
	}
}
