using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TElemento
    {
        public TElemento()
        {
            TAnuncioElemento = new HashSet<TAnuncioElemento>();
        }

        public int Id { get; set; }
        public int IdElementoSubCategoria { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TElementoSubCategoria IdElementoSubCategoriaNavigation { get; set; }
        public virtual ICollection<TAnuncioElemento> TAnuncioElemento { get; set; }
    }
}
