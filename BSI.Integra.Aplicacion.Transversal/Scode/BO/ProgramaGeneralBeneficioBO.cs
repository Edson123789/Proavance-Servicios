using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProgramaGeneralBeneficioBO: BaseBO
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public List<ProgramaGeneralBeneficioModalidadBO> ProgramaGeneralBeneficioModalidad { get; set; }
        public List<ProgramaGeneralBeneficioArgumentoBO> programaGeneralBeneficioArgumento { get; set; }

    }
}
