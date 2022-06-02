using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class ProgramaGeneralBeneficioArgumentoBO : BaseBO
    {
        public int IdProgramaGeneralBeneficio { get; set; }
        public string Nombre { get; set; }
        public int IdPgeneral { get; set; }
    }
}
