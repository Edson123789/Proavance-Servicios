using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCampaniaGeneralDetalleSubArea
    {
        public int Id { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdCampaniaGeneralDetalle { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TCampaniaGeneralDetalle IdCampaniaGeneralDetalleNavigation { get; set; }
        public virtual TSubAreaCapacitacion IdSubAreaCapacitacionNavigation { get; set; }
    }
}
