using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralPrerequisitoModalidadBO : BaseBO
    {
        public int IdProgramaGeneralPrerequisito { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; }
        public int IdPgeneral { get; set; }

		public Guid? IdMigracion { get; set; }
	}
}
