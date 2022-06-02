using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTipoDescuentoAsesorCoordinadorPw
    {
        public int Id { get; set; }
        public int IdAgendaTipoUsuario { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TTipoDescuento IdTipoDescuentoNavigation { get; set; }
    }
}
