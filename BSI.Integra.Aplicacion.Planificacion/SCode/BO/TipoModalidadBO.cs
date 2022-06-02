using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class TipoModalidadBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Abreviatura { get; set; }
        public string ImagenPrincipal { get; set; }
        public string ImagenPrincipalAlf { get; set; }
        public string ImagenSecundaria { get; set; }
        public string ImagenSecundariaAlf { get; set; }
        public string DescripcionCorta { get; set; }
        public string Descripcion { get; set; }
        public string Preguntas { get; set; }

    }
}
