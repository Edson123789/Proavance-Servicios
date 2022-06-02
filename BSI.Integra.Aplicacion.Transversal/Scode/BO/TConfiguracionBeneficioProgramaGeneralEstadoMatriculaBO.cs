using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TConfiguracionBeneficioProgramaGeneralEstadoMatriculaBO : BaseBO
    {
        public int IdConfiguracionBeneficioPGneral { get; set; }
        public int IdEstadoMatricula { get; set; }
    }
}
