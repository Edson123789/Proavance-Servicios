using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class FiltroSegmentoCalculadoBO : BaseBO
    {
        public int IdFiltroSegmento { get; set; }
        public int IdAlumno { get; set; }
        public bool? TieneVentaCruzada { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdOportunidad { get; set; }

    }
}
