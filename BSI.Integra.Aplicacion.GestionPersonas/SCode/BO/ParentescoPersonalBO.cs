using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class ParentescoPersonalBO : BaseBO
	{
		public string Nombre { get; set; }
		public string Comentario { get; set; }
		public int? IdMigracion { get; set; }
	}
}
