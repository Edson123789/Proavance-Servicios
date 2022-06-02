using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ConfiguracionCoordinadorDTO
	{
		public int Id { get; set; }
		public int [] ListaPersonal { get; set; }
		public int [] ListaCentroCosto { get; set; }
		public int [] ListaEstadoMatricula { get; set; }
		public int [] ListaSubEstadoMatricula { get; set; }
		public string Usuario { get; set; }
	}
	public class ConfiguracionCoordinadorAgrupadoDTO
	{
		public int Id { get; set; }
		public int IdPersonal { get; set; }
		public List<ConfiguracionCoordinadorAgrupadoModalidadDTO> DetalleModalidad { get; set; }

	}
	public class  ConfiguracionCoordinadorAgrupadoModalidadDTO
	{
		public int[] ListaModalidad { get; set; }
		public List<ConfiguracionCoordinadorAgrupadoPaisDTO> DetallePais { get; set; }

	}
	public class ConfiguracionCoordinadorAgrupadoPaisDTO
	{
		public int[] ListaPais { get; set; }
	}
}
