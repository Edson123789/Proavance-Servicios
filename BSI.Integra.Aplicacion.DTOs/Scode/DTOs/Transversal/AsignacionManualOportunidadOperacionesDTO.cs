using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class AsignacionManualOportunidadOperacionesDTO
	{
		public int Id { get; set; }
		public string CentroCosto { get; set; }
		public string Alumno { get; set; }
		public string Email { get; set; }
		public string Personal { get; set; }
		public string CodigoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public string UsuarioModificacion { get; set; }
		public DateTime FechaModificacion { get; set; }
	}

	public class AsignacionManualOportunidadOperacionesFiltroDTO
	{
		public List<int> ListaPersonal { get; set; }
		public List<int> ListaCentroCosto { get; set; }
        public List<int> ListaEstados { get; set; }
        public List<int> ListaSubestados { get; set; }
        public string Email { get; set; }
		public string CodigoMatricula { get; set; }
		public List<string> ListaCodigoMatricula { get; set; }
		public int Personal { get; set; }

	}
	public class AsignacionManualOportunidadOperacionesFiltroGrillaDTO
	{
		public Paginador Paginador { get; set; }
		public GridFilters Filter { get; set; }
		public AsignacionManualOportunidadOperacionesFiltroDTO Filtro { get; set; }		

	}

	public class ResultadoFiltroAsignacionOportunidadDTO
	{
		public List<AsignacionManualOportunidadOperacionesDTO> Lista { get; set; }
		public int Total { get; set; }
	}
	public class TotalOportunidadAsignacionManualOperacionesDTO
	{
		public int Cantidad { get; set; }
	}

	public class AsignarOportunidadOperacionesFiltroDTO
	{
		public int IdPersonal { get; set; }
		public List<int> ListaOportunidades { get; set; }
		public string Usuario { get; set; }
	}
}
