using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TClasificacionPersona
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdTablaOriginal { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPersona IdPersonaNavigation { get; set; }
        public virtual TTipoPersona IdTipoPersonaNavigation { get; set; }
    }
}
