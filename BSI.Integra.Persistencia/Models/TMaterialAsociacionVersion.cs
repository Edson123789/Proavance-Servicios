using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialAsociacionVersion
    {
        public int Id { get; set; }
        public int IdMaterialTipo { get; set; }
        public int IdMaterialVersion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMaterialTipo IdMaterialTipoNavigation { get; set; }
        public virtual TMaterialVersion IdMaterialVersionNavigation { get; set; }
    }
}
