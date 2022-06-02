using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCloudflareFirmaVideo
    {
        public int Id { get; set; }
        public string JsonEnvio { get; set; }
        public string JsonRespuesta { get; set; }
        public string VideoId { get; set; }
        public string Uid { get; set; }
        public string Thumbnail { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int Size { get; set; }
        public bool RequireSignedUrls { get; set; }
        public int Duration { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
