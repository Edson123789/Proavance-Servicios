using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class FiltroSegmentoValorTipoBO : BaseBO
    {
        public int IdFiltroSegmento { get; set; }
        public int Valor { get; set; }
        public int? IdMigracion { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public FiltroSegmentoValorTipoBO() {
        }
    }
}
