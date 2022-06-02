using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCloudflareLlaveGenerada
    {
        public int Id { get; set; }
        public int IdCloudflareUsuarioLlave { get; set; }
        public string JsonRespuesta { get; set; }
        public string KeyId { get; set; }
        public string KeyPem { get; set; }
        public string KeyJwk { get; set; }
        public string Created { get; set; }
        public bool Success { get; set; }
        public bool Valido { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
