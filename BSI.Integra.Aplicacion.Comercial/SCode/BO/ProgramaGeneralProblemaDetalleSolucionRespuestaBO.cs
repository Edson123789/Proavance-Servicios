using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class ProgramaGeneralProblemaDetalleSolucionRespuestaBO: BaseBO
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralProblemaDetalleSolucion { get; set; }
        public bool EsSeleccionado { get; set; }
        public bool EsSolucionado { get; set; }
        public int? IdMigracion { get; set; }
    }
}
