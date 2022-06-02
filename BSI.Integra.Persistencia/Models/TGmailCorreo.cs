using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TGmailCorreo
    {
        public TGmailCorreo()
        {
            TGmailCorreoArchivoAdjunto = new HashSet<TGmailCorreoArchivoAdjunto>();
        }

        public int Id { get; set; }
        public int? IdEtiqueta { get; set; }
        public int? IdGmailCliente { get; set; }
        public string IdCorreoGmailFormat { get; set; }
        public string Asunto { get; set; }
        public DateTime? Fecha { get; set; }
        public string EmailBody { get; set; }
        public bool? Seen { get; set; }
        public string Remitente { get; set; }
        public string Destinatarios { get; set; }
        public int? IdPersonal { get; set; }
        public int? Filas { get; set; }
        public int? IdInteraccion { get; set; }
        public string Cc { get; set; }
        public string ResumenMensaje { get; set; }
        public string Bcc { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdClasificacionPersona { get; set; }

        public virtual ICollection<TGmailCorreoArchivoAdjunto> TGmailCorreoArchivoAdjunto { get; set; }
    }
}
