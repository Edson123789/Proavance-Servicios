using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	public class RaJerarquiaBO : BaseBO
	{
		public int IdUsuarioSubordinado { get; set; }
		public int IdUsuarioJefe { get; set; }
		public int? IdMigracion { get; set; }
	}
}
