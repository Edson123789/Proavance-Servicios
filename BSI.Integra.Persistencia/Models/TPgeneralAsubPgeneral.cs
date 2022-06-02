using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPgeneralAsubPgeneral
    {
        public TPgeneralAsubPgeneral()
        {
            TPgeneralAsubPgeneralVersionPrograma = new HashSet<TPgeneralAsubPgeneralVersionPrograma>();
        }

        public int Id { get; set; }
        public int IdPgeneralPadre { get; set; }
        public int IdPgeneralHijo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? Orden { get; set; }

        public virtual ICollection<TPgeneralAsubPgeneralVersionPrograma> TPgeneralAsubPgeneralVersionPrograma { get; set; }
    }
}
