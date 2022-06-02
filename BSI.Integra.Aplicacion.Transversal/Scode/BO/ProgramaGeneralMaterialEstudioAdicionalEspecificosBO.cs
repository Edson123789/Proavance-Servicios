using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralMaterialEstudioAdicionalEspecificosBO : BaseBO
    {
        public int MaterialEstudioAdicionalPorPgeneralId { get; set; }
        public int IdPEspecifico { get; set; }
    }

    public class RegistroProgramaGeneralMaterialEstudioAdicionalEspecificosBO
    {
        public int IdPEspecifico { get; set; }
    }
}
