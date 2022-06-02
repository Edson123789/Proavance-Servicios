using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
    public class PuestoTrabajoRelacionInternaBO : BaseBO
    {
		public int IdPerfilPuestoTrabajo { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
