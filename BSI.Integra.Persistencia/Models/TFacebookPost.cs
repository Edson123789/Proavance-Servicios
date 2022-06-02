using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFacebookPost
    {
        public int Id { get; set; }
        public string Link { get; set; }
        public string Message { get; set; }
        public string PermalinkUrl { get; set; }
        public string UrlPicture { get; set; }
        public string IdPostFacebook { get; set; }
        public int? IdPgeneral { get; set; }
        public string ConjuntoAnuncioIdFacebook { get; set; }
        public string IdAnuncioFacebook { get; set; }
        public string CentroCosto { get; set; }
        public string CentroCosto2 { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
