using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class ExamenAsignadoEvaluadorBO : BaseBO
	{
		public int IdPersonal { get; set; }
		public int IdPostulante { get; set; }
		public int IdExamen { get; set; }
		public int IdProcesoSeleccion { get; set; }
		public bool EstadoExamen { get; set; }
		public int? IdMigracion { get; set; }
	}
}
