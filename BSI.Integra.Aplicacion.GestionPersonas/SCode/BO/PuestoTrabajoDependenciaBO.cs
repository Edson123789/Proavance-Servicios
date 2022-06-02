using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class PuestoTrabajoDependenciaBO : BaseBO
	{
		public int IdPuestoTrabajo { get; set; }
		public int IdPerfilPuestoTrabajo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
