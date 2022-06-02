using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class CompetidorTipoModalidadBO : BaseBO
    {
        public int Id { get; set; }
        public int IdCompetidor { get; set; }
        public int IdTipoModalidad { get; set; }

    }
}
