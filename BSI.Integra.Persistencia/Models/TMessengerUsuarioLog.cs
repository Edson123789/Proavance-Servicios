using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMessengerUsuarioLog
    {
        public int Id { get; set; }
        public int? IdMessengerUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TAreaCapacitacionFacebook IdAreaCapacitacionFacebookNavigation { get; set; }
        public virtual TMessengerUsuario IdMessengerUsuarioNavigation { get; set; }
        public virtual TPersonal IdPersonalNavigation { get; set; }
    }
}
