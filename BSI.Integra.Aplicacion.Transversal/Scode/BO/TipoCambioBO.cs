using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class TipoCambioBO : BaseBO
    {
        public double SolesDolares { get; set; }
        public double DolaresSoles { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdMigracion { get; set; }

        public TipoCambioBO() {
        }
    }
}
