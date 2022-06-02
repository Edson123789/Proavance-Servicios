using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSeccionTipoDetallePw
    {
        public int Id { get; set; }
        public int IdSeccionPw { get; set; }
        public string NombreTitulo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdSeccionTipoContenido { get; set; }

        public virtual TSeccionPw IdSeccionPwNavigation { get; set; }
    }
}
