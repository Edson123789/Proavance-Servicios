using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAnuncioElemento
    {
        public int Id { get; set; }
        public int IdAnuncio { get; set; }
        public int IdElemento { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TAnuncio IdAnuncioNavigation { get; set; }
        public virtual TElemento IdElementoNavigation { get; set; }
    }
}
