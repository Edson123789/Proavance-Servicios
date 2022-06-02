using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralMotivacionModalidadBO: BaseBO
    {
        public int IdProgramaGeneralMotivacion { get; set; }
        public int IdModalidadCurso { get; set; }
        public string Nombre { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
}
