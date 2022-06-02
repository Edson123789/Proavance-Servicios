using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFacebookUsuarioOportunidad
    {
        public int Id { get; set; }
        public string Psid { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPersonal { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdMessengerChat { get; set; }
    }
}
