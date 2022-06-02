using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MaterialEntregaBO : BaseBO
    {
        public int IdMaterialVersion { get; set; }
        public int IdAsistencia { get; set; }
        public bool Entregado { get; set; }
        public string Comentario { get; set; }
        public int? IdMigracion { get; set; }
    }
}
