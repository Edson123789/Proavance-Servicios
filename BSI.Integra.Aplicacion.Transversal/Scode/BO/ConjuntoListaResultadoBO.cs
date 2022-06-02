using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ConjuntoListaResultadoBO : BaseBO
    {
        public int IdAlumno { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public bool? EsVentaCruzada { get; set; }
        public int NroEjecucion { get; set; }
        public bool Activo { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdOportunidad { get; set; }

        public ConjuntoListaResultadoBO() {
        }
    }
}
