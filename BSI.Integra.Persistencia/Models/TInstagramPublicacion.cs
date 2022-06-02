using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInstagramPublicacion
    {
        public TInstagramPublicacion()
        {
            TInstagramComentario = new HashSet<TInstagramComentario>();
        }

        public int Id { get; set; }
        public string InstagramId { get; set; }
        public string Subtitulo { get; set; }
        public string TipoMedia { get; set; }
        public string UrlMedia { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TInstagramComentario> TInstagramComentario { get; set; }
    }
}
