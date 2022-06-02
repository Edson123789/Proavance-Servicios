using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralProblemaBO: BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public int? IdMigracion { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<ProgramaGeneralProblemaModalidadBO> ProgramaGeneralProblemaModalidad { get; set; }
        public List<ProgramaGeneralProblemaDetalleSolucionBO> programaGeneralProblemaDetalleSolucion { get; set; }

    }
}
