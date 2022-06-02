using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CampaniaMailchimpBO : BaseBO
    {
        public string MailchimpId { get; set; }
        public int MailchimpWebId { get; set; }
        public string MailchimpTipo { get; set; }
        public DateTime MailchimpFechaCreacion { get; set; }
        public string MailchimpEstado { get; set; }
        public int? CantidadEnvio { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string TipoContenido { get; set; }
        public string MailchimpListaId { get; set; }
        public string MailchimpListaNombre { get; set; }
        public int CantidadRecipiente { get; set; }
        public string MailchimpAsunto { get; set; }
        public string MailchimpTitulo { get; set; }
        public string NombreRemitente { get; set; }
        public string CorreoRemitente { get; set; }
        public int? CantidadApertura { get; set; }
        public int? CantidadAperturaUnica { get; set; }
        public decimal? TasaApertura { get; set; }
        public int? CantidadClic { get; set; }
        public int? CantidadClicSuscriptor { get; set; }
        public decimal? TasaClic { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public CampaniaMailchimpBO()
        {
        }

        public CampaniaMailchimpBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
