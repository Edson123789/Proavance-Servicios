using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralProblemaDetalleSolucionBO: BaseBO
    {
        public int IdProgramaGeneralProblema { get; set; }
        public string Detalle { get; set; }
        public string Solucion { get; set; }
        public int IdPgeneral { get; set; }
        public int? IdMigracion { get; set; }
    }
}
