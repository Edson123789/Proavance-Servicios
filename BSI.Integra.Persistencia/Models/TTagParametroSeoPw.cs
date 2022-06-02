using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTagParametroSeoPw
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdTagPw { get; set; }
        public int IdParametroSeopw { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TParametroSeoPw IdParametroSeopwNavigation { get; set; }
        public virtual TTagPw IdTagPwNavigation { get; set; }
    }
}
