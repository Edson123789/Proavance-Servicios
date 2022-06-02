using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TWebinarCentroCosto
    {
        public int Id { get; set; }
        public int IdWebinar { get; set; }
        public int IdCentroCosto { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TCentroCosto IdCentroCostoNavigation { get; set; }
        public virtual TWebinar IdWebinarNavigation { get; set; }
    }
}
