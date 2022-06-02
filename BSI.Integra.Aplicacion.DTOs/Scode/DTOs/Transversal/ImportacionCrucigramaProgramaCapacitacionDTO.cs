using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ImportacionCrucigramaProgramaCapacitacionDTO
	{
		public string CodigoCrucigrama { get; set; }
		public int IdPGeneral { get; set; }
		public int IdPEspecifico { get; set; }
		public int? OrdenFilaCapitulo { get; set; }
		public string Sesion { get; set; }
		public string SubSeccion { get; set; }
		public int IdTipoMarcador { get; set; }
		public decimal ValorMarcador { get; set; }
		public int CantidadFila { get; set; }
		public int CantidadColumna { get; set; }

		//============CRUCIGRAMA DETALLE=============================

		public int NumeroPalabra { get; set; }
		public string Palabra { get; set; }
		public string Definicion { get; set; }
		public int Tipo { get; set; }
		public int ColumnaInicio { get; set; }
		public int FilaInicio { get; set; }
	}
    public class SubidaArchivosDTO
    {
        public string Nombre { get; set; }
        public int IdPlantillaFrontal { get; set; }
        public int IdPlantillaPosterior { get; set; }
        public int IdOperadorComparacion { get; set; }
        public int TieneDeuda { get; set; }
        public string IdModalidad { get; set; }
        public string ProgramasGenerales { get; set; }
        public string Estados { get; set; }
        public string SubEstados { get; set; }
    }

    public class CrucigramaProgramaCapacitacionExcelCompuestoDTO
	{
		public CrucigramaProgramaCapacitacionAgrupadoDTO CrucigramaProgramaCapacitacion { get; set; }
	}
	public class CrucigramaProgramaCapacitacionAgrupadoDTO
	{
		public string CodigoCrucigrama { get; set; }
		public int IdPGeneral { get; set; }
		public int IdPEspecifico { get; set; }
		public int? OrdenFilaCapitulo { get; set; }
		public string Sesion { get; set; }
		public string SubSeccion { get; set; }
		public int IdTipoMarcador { get; set; }
		public decimal ValorMarcador { get; set; }
		public int CantidadFila { get; set; }
		public int CantidadColumna { get; set; }
		public List<CrucigramaDetalleProgramaCapacitacionAgrupadoDTO> CrucigramaDetalleProgramaCapacitacion { get; set; }
	}

	public class CrucigramaDetalleProgramaCapacitacionAgrupadoDTO
	{
		public int NumeroPalabra { get; set; }
		public string Palabra { get; set; }
		public string Definicion { get; set; }
		public int Tipo { get; set; }
		public int ColumnaInicio { get; set; }
		public int FilaInicio { get; set; }
	}
}
