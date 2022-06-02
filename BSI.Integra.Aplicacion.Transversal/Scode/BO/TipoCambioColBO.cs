using System;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class TipoCambioColBO : BaseBO
    {
        public double PesosDolares { get; set; }
        public double DolaresPesos { get; set; }
        public DateTime Fecha { get; set; }
        public int IdMoneda { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
