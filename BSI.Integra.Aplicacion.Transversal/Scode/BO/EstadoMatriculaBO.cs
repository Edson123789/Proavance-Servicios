using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public partial class EstadoMatriculaBO : BaseBO
    {
        public string EstadoMatricula { get; set; }
        public int? IdMigracion { get; set; }
    }
}
