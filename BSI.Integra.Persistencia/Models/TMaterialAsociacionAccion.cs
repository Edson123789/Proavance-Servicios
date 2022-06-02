using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialAsociacionAccion
    {
        public int Id { get; set; }
        public int IdMaterialTipo { get; set; }
        public int IdMaterialAccion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TMaterialAccion IdMaterialAccionNavigation { get; set; }
        public virtual TMaterialTipo IdMaterialTipoNavigation { get; set; }
    }
}
