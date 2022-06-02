using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TParametroSeoPw
    {
        public TParametroSeoPw()
        {
            TAreaParametroSeoPw = new HashSet<TAreaParametroSeoPw>();
            TSubAreaParametroSeoPw = new HashSet<TSubAreaParametroSeoPw>();
            TTagParametroSeoPw = new HashSet<TTagParametroSeoPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int NumeroCaracteres { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TAreaParametroSeoPw> TAreaParametroSeoPw { get; set; }
        public virtual ICollection<TSubAreaParametroSeoPw> TSubAreaParametroSeoPw { get; set; }
        public virtual ICollection<TTagParametroSeoPw> TTagParametroSeoPw { get; set; }
    }
}
