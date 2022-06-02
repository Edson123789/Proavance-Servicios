using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRevisionNivelPw
    {
        public TRevisionNivelPw()
        {
            TBandejaPendientePw = new HashSet<TBandejaPendientePw>();
            TPlantillaRevisionPw = new HashSet<TPlantillaRevisionPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public int IdTipoRevisionPw { get; set; }
        public int IdRevisionPw { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TRevisionPw IdRevisionPwNavigation { get; set; }
        public virtual TTipoRevisionPw IdTipoRevisionPwNavigation { get; set; }
        public virtual ICollection<TBandejaPendientePw> TBandejaPendientePw { get; set; }
        public virtual ICollection<TPlantillaRevisionPw> TPlantillaRevisionPw { get; set; }
    }
}
