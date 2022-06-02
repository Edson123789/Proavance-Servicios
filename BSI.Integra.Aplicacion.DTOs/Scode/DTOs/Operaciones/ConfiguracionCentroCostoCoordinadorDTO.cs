using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ConfiguracionCentroCostoCoordinadorDTO
	{
		public int? Id { get; set; }
		public int? IdPersonal { get; set; }
		public string Personal { get; set; }
		public int? IdEstadoMatricula { get; set; }
		public string EstadoMatricula { get; set; }
		public int? IdSubEstadoMatricula { get; set; }
		public string SubEstadoMatricula { get; set; }
		public int IdCentroCosto { get; set; }
		public int IdProgramaEspecifico { get; set; }
		public string CentroCosto { get; set; }
		public string ProgramaEspecifico { get; set; }
		public string EstadoProgramaEspecifico { get; set; }
		public string Tipo { get; set; }
		public DateTime FechaCreacion { get; set; }
		public bool EsAsignado { get; set; }
	}

	public class ConfiguracionCoordinadorPorPersonal
	{
		public int IdPersonal { get; set; }
		public string Personal { get; set; }
        public int? IdEstadoMatricula { get; set; }
        public string EstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public string SubEstadoMatricula { get; set; }
        public List<ConfiguracionCoordinadorPorPersonalDetalleCentroCosto> DetalleCentroCosto { get; set; }
        public List<ConfiguracionCoordinadorPorPersonalDetalleEstadoMatricula> DetalleEstadoMatricula { get; set; }
        public List<ConfiguracionCoordinadorPorPersonalDetalleSubEstadoMatricula> DetalleSubEstadoMatricula { get; set; }
    }

	public class ConfiguracionCoordinadorPorPersonalDetalleCentroCosto
	{
		public int IdCentroCosto { get; set; }
		public string CentroCosto { get; set; }
	}
	public class ConfiguracionCoordinadorPorPersonalDetalleEstadoMatricula
	{
		public int? IdEstadoMatricula{ get; set; }
		public string EstadoMatricula { get; set; }
	}
	public class ConfiguracionCoordinadorPorPersonalDetalleSubEstadoMatricula
	{
		public int? IdSubEstadoMatricula { get; set; }
		public string SubEstadoMatricula { get; set; }
	}
}
