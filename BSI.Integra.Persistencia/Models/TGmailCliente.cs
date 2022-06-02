using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TGmailCliente
    {
        public int Id { get; set; }
        public int? IdAsesor { get; set; }
        public string EmailAsesor { get; set; }
        public string PasswordCorreo { get; set; }
        public string NombreAsesor { get; set; }
        public string IdClient { get; set; }
        public string ClientSecret { get; set; }
        public string AliasEmailAsesor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
