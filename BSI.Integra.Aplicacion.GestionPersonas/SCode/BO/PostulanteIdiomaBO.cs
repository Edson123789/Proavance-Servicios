using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class PostulanteIdiomaBO : BaseBO
	{
		public int IdPostulante { get; set; }
		public int IdIdioma { get; set; }
		public int IdNivelIdioma { get; set; }
		public int? IdMigracion { get; set; }
	}
}
