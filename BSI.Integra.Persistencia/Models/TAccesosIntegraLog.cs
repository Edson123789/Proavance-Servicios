using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAccesosIntegraLog
    {
        public TAccesosIntegraLog()
        {
            TAccesosIntegraDetalleLog = new HashSet<TAccesosIntegraDetalleLog>();
        }

        public int Id { get; set; }
        public string Usuario { get; set; }
        public string IpUsuario { get; set; }
        public string Cookie { get; set; }
        public bool Habilitado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual ICollection<TAccesosIntegraDetalleLog> TAccesosIntegraDetalleLog { get; set; }
    }
}
