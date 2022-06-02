using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class ProgramaCapacitacionPorPGeneralBO : BaseBO
    {
        public int Id { get; set; }
        public int IdProgramaCapacitacion { get; set; }
        public int IdPGeneral { get; set; }

    }
}
