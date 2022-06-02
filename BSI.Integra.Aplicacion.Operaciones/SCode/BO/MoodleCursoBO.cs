using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class MoodleCursoBO : BaseBO
    {
		public int IdCursoMoodle { get; set; }
		public int IdCategoriaMoodle { get; set; }
		public string Nombre { get; set; }
		public int? IdMigracion { get; set; }

	}
}
