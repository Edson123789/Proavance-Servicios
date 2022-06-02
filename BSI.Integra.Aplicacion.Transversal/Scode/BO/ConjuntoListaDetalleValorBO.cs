using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConjuntoListaDetalleValorBO : BaseBO
    {
        public int Valor { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int IdCategoriaObjetoFiltro { get; set; }
        public int? IdMigracion { get; set; }
        public ConjuntoListaDetalleValorBO() {
        }
    }
}
