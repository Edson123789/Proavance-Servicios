using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class AsesorProbabilidadDetalleBO : BaseBO
    {
        public int IdAsesorCentroCosto { get; set; }
        public int IdProbabilidadRegistroPw { get; set; }
        public int Prioridad { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
