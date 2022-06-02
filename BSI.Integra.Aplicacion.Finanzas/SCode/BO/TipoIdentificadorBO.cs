using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
	public partial class TipoIdentificadorBO : BaseBO
	{
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public int IdPais { get; set; }
		public Guid? IdMigracion { get; set; }
	}
}
