using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TUsuario
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public int IdUsuarioRol { get; set; }
        public string CodigoAreaTrabajo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPersonal IdPersonalNavigation { get; set; }
        public virtual TUsuarioRol IdUsuarioRolNavigation { get; set; }
    }
}
