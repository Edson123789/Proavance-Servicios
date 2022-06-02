using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionEnlaceDetalleMailChimp
    {
        public int Id { get; set; }
        public int IdInteraccionEnlaceMailChimp { get; set; }
        public int CantidadClicks { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TInteraccionEnlaceMailchimp IdInteraccionEnlaceMailChimpNavigation { get; set; }
    }
}
