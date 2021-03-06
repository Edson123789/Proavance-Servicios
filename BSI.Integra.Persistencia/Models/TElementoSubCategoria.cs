using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TElementoSubCategoria
    {
        public TElementoSubCategoria()
        {
            TElemento = new HashSet<TElemento>();
        }

        public int Id { get; set; }
        public int IdElementoCategoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TElementoCategoria IdElementoCategoriaNavigation { get; set; }
        public virtual ICollection<TElemento> TElemento { get; set; }
    }
}
