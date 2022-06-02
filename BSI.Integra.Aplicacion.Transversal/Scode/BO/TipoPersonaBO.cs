using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class TipoPersonaBO : BaseBO
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string NombreTablaOriginal { get; set; }
        public int? IdMigracion { get; set; }
    }
}
