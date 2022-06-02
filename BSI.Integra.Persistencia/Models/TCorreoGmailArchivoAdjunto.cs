using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCorreoGmailArchivoAdjunto
    {
        public int Id { get; set; }
        public int IdCorreoGmail { get; set; }
        public string Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string UrlArchivoRepositorio { get; set; }

        public virtual TCorreoGmail IdCorreoGmailNavigation { get; set; }
    }
}
