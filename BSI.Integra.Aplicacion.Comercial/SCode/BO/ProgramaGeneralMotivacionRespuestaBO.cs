using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class ProgramaGeneralMotivacionRespuestaBO : BaseBO
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralMotivacion { get; set; }
        public int Respuesta { get; set; }
        public int? IdMigracion { get; set; }

    }
}
