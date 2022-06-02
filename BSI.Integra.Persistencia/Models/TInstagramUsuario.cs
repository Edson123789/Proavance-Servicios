using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInstagramUsuario
    {
        public TInstagramUsuario()
        {
            TInstagramComentario = new HashSet<TInstagramComentario>();
        }

        public int Id { get; set; }
        public string Usuario { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TInstagramComentario> TInstagramComentario { get; set; }
    }
}
