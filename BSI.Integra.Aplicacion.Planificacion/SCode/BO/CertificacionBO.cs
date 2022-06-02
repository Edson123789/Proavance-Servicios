using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class CertificacionBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPartner { get; set; }
        public int Costo { get; set; }
        public int IdMoneda { get; set; }
        public int IdCertificacionTipo { get; set; }
        
    }
}
