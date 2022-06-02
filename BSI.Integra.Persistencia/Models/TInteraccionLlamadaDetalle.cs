using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInteraccionLlamadaDetalle
    {
        public int Id { get; set; }
        public int? IdInteraccion { get; set; }
        public string NumeroTelefono { get; set; }
        public int? Duracion { get; set; }
        public int? IdCentralLlamada { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
