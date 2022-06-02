using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaMailchimp
    {
        public int Id { get; set; }
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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
