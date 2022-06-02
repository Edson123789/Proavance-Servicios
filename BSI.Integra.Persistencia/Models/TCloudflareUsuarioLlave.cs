using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCloudflareUsuarioLlave
    {
        public int Id { get; set; }
        public string AuthKey { get; set; }
        public string AuthEmail { get; set; }
        public string AccountId { get; set; }
        public int? IdPersonal { get; set; }
        public bool Activar { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
