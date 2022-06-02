using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProveedorTipoServicioBO : BaseBO
    {
        public int IdProveedor { get; set; }
        public int IdTipoServicio { get; set; }
        public int? IdMigracion { get; set; }
        public virtual ProveedorBO Proveedor{ get; set; }
        public virtual TipoServicioBO TipoServicio { get; set; }
    }
}
