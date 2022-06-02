using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Persistencia.Models;
using System;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class MiembroMailchimpBO : BaseBO
    {
        public int IdListaMailchimp { get; set; }
        public string MailchimpId { get; set; }
        public string MailchimpCorreo { get; set; }
        public string MailchimpCorreoId { get; set; }
        public string MailchimpContactoId { get; set; }
        public string NombreCompleto { get; set; }
        public int MailchimpWebId { get; set; }
        public string TipoCorreo { get; set; }
        public string MailchimpEstado { get; set; }
        public decimal PromedioApertura { get; set; }
        public decimal PromedioClic { get; set; }
        public DateTime? FechaOptIn { get; set; }
        public int RatingMiembro { get; set; }
        public DateTime? FechaUltimoCambio { get; set; }
        public string ClienteCorreo { get; set; }
        public string Fuente { get; set; }
        public string MailchimpListaId { get; set; }
        public int? IdMigracion { get; set; }

        private readonly integraDBContext _integraDBContext;

        public MiembroMailchimpBO()
        {
        }

        public MiembroMailchimpBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
    }
}
