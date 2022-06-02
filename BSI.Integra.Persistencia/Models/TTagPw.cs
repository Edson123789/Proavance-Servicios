using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTagPw
    {
        public TTagPw()
        {
            TPgeneralTagsPw = new HashSet<TPgeneralTagsPw>();
            TTagParametroSeoPw = new HashSet<TTagParametroSeoPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? TagWebId { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string Codigo { get; set; }

        public virtual ICollection<TPgeneralTagsPw> TPgeneralTagsPw { get; set; }
        public virtual ICollection<TTagParametroSeoPw> TTagParametroSeoPw { get; set; }
    }
}
