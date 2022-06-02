using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class PerfilPuestoTrabajoPersonalAprobacionBO : BaseBO
	{
		public int IdPersonal { get; set; }
		public int IdPuestoTrabajo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
