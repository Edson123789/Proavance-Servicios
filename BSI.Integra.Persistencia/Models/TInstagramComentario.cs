using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInstagramComentario
    {
        public int Id { get; set; }
        public string InstagramId { get; set; }
        public string Texto { get; set; }
        public DateTime FechaInteraccion { get; set; }
        public int IdInstagramUsuario { get; set; }
        public int IdInstagramPublicacion { get; set; }
        public int IdPersonalAsociado { get; set; }
        public bool EsUsuarioInstagram { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TInstagramPublicacion IdInstagramPublicacionNavigation { get; set; }
        public virtual TInstagramUsuario IdInstagramUsuarioNavigation { get; set; }
        public virtual TPersonal IdPersonalAsociadoNavigation { get; set; }
    }
}
