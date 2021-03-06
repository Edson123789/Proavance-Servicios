using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TAreaCapacitacionFacebook
    {
        public TAreaCapacitacionFacebook()
        {
            TMessengerUsuarioLog = new HashSet<TMessengerUsuarioLog>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual ICollection<TMessengerUsuarioLog> TMessengerUsuarioLog { get; set; }
    }
}
