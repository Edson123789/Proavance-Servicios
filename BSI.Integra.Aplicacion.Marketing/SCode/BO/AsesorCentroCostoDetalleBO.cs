using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class AsesorCentroCostoDetalleBO : BaseBO
    {
        public int IdAsesorCentroCosto { get; set; }
        public int IdCentroCosto { get; set; }
        public int Prioridad { get; set; }
        public Guid? IdMigracion { get; set; }
        public AsesorCentroCostoDetalleBO() {
        }
    }
}
