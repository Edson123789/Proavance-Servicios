using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CantidadInteraccionBO : BaseBO
    {
        public string Nombre { get; set; }
        public byte Cantidad { get; set; }
        public int? IdMigracion { get; set; }
    }
}
