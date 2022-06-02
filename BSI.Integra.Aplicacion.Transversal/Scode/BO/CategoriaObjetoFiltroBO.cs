using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class CategoriaObjetoFiltroBO : BaseBO
    {
        public string Nombre { get; set; }
        public string NombreObjeto { get; set; }
        public bool EsTabla { get; set; }
        public bool AplicaConjuntoLista { get; set; }
        public bool AplicaFiltroSegmento { get; set; }
        public int? IdMigracion { get; set; }
        public CategoriaObjetoFiltroBO() {
        }
    }
}
