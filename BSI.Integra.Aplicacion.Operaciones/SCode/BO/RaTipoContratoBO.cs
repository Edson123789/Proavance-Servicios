using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	public class RaTipoContratoBO : BaseBO
	{
		public string Nombre { get; set; }
		public int? IdMigracion { get; set; }
	}
}
