using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ConfiguracionAsignacionExamenDTO
	{
		public int Id { get; set; }
		public int IdProcesoSeleccion { get; set; }
		public int IdExamen { get; set; }
		public int NroOrden { get; set; }

	}
	public class ConfiguracionAsignacionExamenV2DTO
	{
		public int Id { get; set; }
		public int IdProcesoSeleccion { get; set; }
		public int IdEvaluacion { get; set; }
		public int IdExamen { get; set; }
		public int NroOrden { get; set; }

	}
}
