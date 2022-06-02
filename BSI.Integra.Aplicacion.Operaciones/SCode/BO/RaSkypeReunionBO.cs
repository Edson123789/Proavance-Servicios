using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	public class RaSkypeReunionBO : BaseBO
	{
		public int IdRaCentroCosto { get; set; }
		public string ReunionId { get; set; }
		public string UrlBase { get; set; }
		public bool Activo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
