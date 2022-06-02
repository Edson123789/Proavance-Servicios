using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralMotivacionBO: BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
        public List<ProgramaGeneralMotivacionModalidadBO> ProgramaGeneralMotivacionModalidad { get; set; }
        public List<ProgramaGeneralMotivacionArgumentoBO> programaGeneralMotivacionArgumento { get; set; }

    }
}
