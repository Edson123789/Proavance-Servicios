using BSI.Integra.Aplicacion.Base.BO;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public partial class AsesorFaseOpoProbabilidadDetalleBO : BaseBO
    {
        public int IdAsesorCentroCosto { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int IdProbabilidadRegistroPw { get; set; }
        public string Tipo { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
