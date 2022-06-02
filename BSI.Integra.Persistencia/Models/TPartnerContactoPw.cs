using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPartnerContactoPw
    {
        public int Id { get; set; }
        public int IdPartner { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
