using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerUsuarioCompuestoDTO
    {
        public int Id { get; set; }
        public string PSID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UrlFoto { get; set; }
        public int? IdPersonal { get; set; }
        public bool? SeRespondio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string Mensaje { get; set; }
    }
}
