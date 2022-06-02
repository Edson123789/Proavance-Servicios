using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMiembroMailchimp
    {
        public int Id { get; set; }
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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
