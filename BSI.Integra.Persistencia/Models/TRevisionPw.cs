using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRevisionPw
    {
        public TRevisionPw()
        {
            TPlantillaPw = new HashSet<TPlantillaPw>();
            TRevisionNivelPw = new HashSet<TRevisionNivelPw>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TPlantillaPw> TPlantillaPw { get; set; }
        public virtual ICollection<TRevisionNivelPw> TRevisionNivelPw { get; set; }
    }
}
