using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComnentInsertarFacebookResultDTO
    {
        public int Id { get; set; }
        public string IdUsuarioFacebook { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UrlFoto { get; set; }
        public int? IdPersonal { get; set; }
        public bool? TieneRespuesta { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
