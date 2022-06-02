using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMessengerChat
    {
        public int Id { get; set; }
        public int? IdMeseengerUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public string Mensaje { get; set; }
        public bool? Tipo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string FacebookId { get; set; }
        public DateTime? FechaInteraccion { get; set; }
        public int? IdTipoMensajeMessenger { get; set; }
        public string UrlArchivoAdjunto { get; set; }
        public bool? Leido { get; set; }
        public DateTime? FechaLectura { get; set; }
        public int? IdFacebookAnuncio { get; set; }
    }
}
