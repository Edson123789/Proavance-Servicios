using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoServicioBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
