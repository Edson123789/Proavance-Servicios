using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CrucigramaProgramaCapacitacionDTO
	{
		public int Id { get; set; }
		public string CodigoCrucigrama { get; set; }
		public int IdPGeneral { get; set; }
		public int IdPEspecifico { get; set; }
		public string PGeneral { get; set; }
		public int? IdCapitulo { get; set; }
		public int? IdSesion { get; set; }
		public int IdTipoMarcador { get; set; }
		public decimal ValorMarcador { get; set; }
		public int CantidadFila { get; set; }
		public int CantidadColumna { get; set; }
	}
	public class CrucigramaProgramaCapacitacionDetalleDTO
	{
		public int Id { get; set; }
		public int NumeroPalabra { get; set; }
		public string Palabra { get; set; }
		public string Definicion { get; set; }
		public int Tipo { get; set; }
		public int ColumnaInicio { get; set; }
		public int FilaInicio { get; set; }
	}

	public class CompuestoCrucigramaProgramaCapacitacionDTO
	{
		public CrucigramaProgramaCapacitacionDTO Crucigrama { get; set; }
		public List<CrucigramaProgramaCapacitacionDetalleDTO> CrucigramaDetalle { get; set; }
		public string Usuario { get; set; }
	}

	public class CrucigramaProgramaCapacitacionSesion
	{
		public int Id { get; set; }
		public int IdPGeneral { get; set; }
		public int OrdenFilaCapitulo { get; set; }
		public int OrdenFilaSesion { get; set; }
		public string CodigoCrucigrama { get; set; }
		public List<CrucigramaProgramaCapacitacionSesionDetalle> CrucigramaDetalle { get; set; }
	}

	public class CrucigramaProgramaCapacitacionSesionDetalle
	{
		public int[] pos { get; set; }
		public int sentido { get; set; }
		public string palabra { get; set; }
		public string pista { get; set; }
	}
}
