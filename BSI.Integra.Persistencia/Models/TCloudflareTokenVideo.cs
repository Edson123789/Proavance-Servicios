using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCloudflareTokenVideo
    {
        public int Id { get; set; }
        public int IdCloudflareLlaveGenerada { get; set; }
        public string JsonEnvio { get; set; }
        public string KeyId { get; set; }
        public string KeyPem { get; set; }
        public string FechaNbf { get; set; }
        public string FechaExp { get; set; }
        public string VideoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string TokenGenerado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
