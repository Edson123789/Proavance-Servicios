using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class LogFiltroSegmentoEjecutadoBO : BaseBO
    {
        public int IdFiltroSegmento { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdTipoDato { get; set; }
        public int IdOrigen { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int TotalOportunidadesCreadas { get; set; }
        public int? IdMigracion { get; set; }

        public LogFiltroSegmentoEjecutadoBO() {
        }
    }
}
