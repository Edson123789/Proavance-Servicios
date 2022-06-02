using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ConfiguracionCoordinadoraCentroCostoDTO
	{
		public int IdPersonal { get; set; }
		public string UsuarioPersonal { get; set; }
		public int? IdEstadoMatricula { get; set; }
		public int? IdSubEstadoMatricula { get; set; }

	}
	public class ConfiguracionCoordinadoraCentroCostoCantidadDTO
	{
		public int IdPersonal { get; set; }
		public string UsuarioPersonal { get; set; }
		public int IdPespecifico { get; set; }
		public int Cantidad { get; set; }
	}
	public class ConfiguracionCoordinadoraSubEstadoMatricula
	{
		public int IdMatriculaCabecera { get; set; }
		public int IdSubEstado { get; set; }
	}
}
