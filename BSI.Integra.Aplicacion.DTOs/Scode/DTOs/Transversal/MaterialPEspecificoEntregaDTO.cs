using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class MaterialPEspecificoEntregaDTO
	{
		public int IdMaterialPEspecifico { get; set; }
		public int Grupo { get; set; }
		public int IdPEspecifico { get; set; }
		public string NombrePEspecifico { get; set; }
		public int IdMaterialTipo { get; set; }
		public string NombreMaterialTipo { get; set; }
		public int IdMaterialAccion { get; set; }
		public string MaterialAccion { get; set; }
		public int IdMaterialPEspecificoDetalle { get; set; }
		public int IdMaterialVersion { get; set; }
		public string MaterialVersion { get; set; }
		public List<MaterialDetalleCriterioVerificacionDTO> CriteriosVerificacion { get; set; }
		public int IdMaterialEstado { get; set; }
		public int? IdEstadoRegistroMaterial { get; set; }
		public string EstadoRegistroMaterial { get; set; }
		public int? IdFur { get; set; }
	}

	public class MaterialPEspecificoFiltroDTO
	{
		public int IdPEspecifico { get; set; }
		public int Grupo { get; set; }
	}

	public class CriterioVerificacionDTO
	{
		public int IdMaterialCriterioVerificacion { get; set; }
		public string MaterialCriterioVerificacion { get; set; }
		public int IdMaterialTipo { get; set; }
	}

	public class CriterioVerificacionColumnasDTO
	{
		public int IdMaterialCriterioVerificacion { get; set; }
		public string MaterialCriterioVerificacion { get; set; }
		public bool EsAprobado { get; set; }
	}
	public class MaterialDetalleCriterioVerificacionDTO
	{
		public int Id { get; set; }
		public int IdMaterialPEspecificoDetalle { get; set; }
		public int IdMaterialCriterioVerificacion { get; set; }
		public string MaterialCriterioVerificacion { get; set; }
		public bool EsAprobado { get; set; }
	}

	public class AprobarRechazarRegistroEntregaMaterialDTO
	{
		public int IdMaterialPEspecificoDetalle { get; set; }
		public Dictionary<int, bool> ClaveValor { get; set; }
		public int EstadoRegistroMaterial { get; set; }
		public string Usuario { get; set; }
	}

	public class MaterialPEspecificoDetalleCriteriosDTO
	{
		public int IdMaterialPEspecificoDetalle { get; set; }
		public int IdMaterialPEspecifico { get; set; }
		public int IdMaterialAccion { get; set; }
		public int IdMaterialVersion { get; set; }
	}

	public class MaterialPEspecificoDetalleEnvioProveedorDTO
	{
		public string NombreProveedor { get; set; }
		public DateTime FechaEntrega { get; set; }
		public string DireccionEntrega { get; set; }
		public decimal Cantidad { get; set; }
		public string NombreMaterialTipo { get; set; }
		public string UrlArchivo { get; set; }
		public int Grupo { get; set; }
		public string NombrePEspecifico { get; set; }
		public string NombreCentroCosto { get; set; }
	}
}
