using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	public class ConfiguracionAsignacionCoordinadorOportunidadOperacionesDetalleBO : BaseBO
	{
		public int IdConfiguracionAsignacionCoordinadorOportunidadOperaciones { get; set; }
		public int IdModalidadCurso { get; set; }
		public int IdPais { get; set; }
		public int IdPersonal { get; set; }
		public int? IdMigracion { get; set; }
		public int? IdCiudad { get; set; }
	}
}
