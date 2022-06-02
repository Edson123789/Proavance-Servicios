using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialPespecifico
    {
        public TMaterialPespecifico()
        {
            TMaterialPespecificoDetalle = new HashSet<TMaterialPespecificoDetalle>();
        }

        public int Id { get; set; }
        public int IdPespecifico { get; set; }
        public int Grupo { get; set; }
        public int IdMaterialTipo { get; set; }
        public int GrupoEdicion { get; set; }
        public int GrupoEdicionOrden { get; set; }
        public int? IdFur { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TMaterialPespecificoDetalle> TMaterialPespecificoDetalle { get; set; }
    }
}
