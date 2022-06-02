using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TEmbudoFiltro
    {
        public int Id { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public string Nivel { get; set; }
        public string SubNivel { get; set; }
        public string SubNivelGrupos { get; set; }
        public string SubNivelFases { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
