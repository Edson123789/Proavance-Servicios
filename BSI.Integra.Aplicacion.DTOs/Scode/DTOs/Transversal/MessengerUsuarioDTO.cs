using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerUsuarioDTO
    {
        public int Id { get; set; }
        public string PSID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UrlFoto { get; set; }
        public int? IdPersonal { get; set; }
        public bool? SeRespondio { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool? MensajeEnviarTelefono { get; set; }
        public bool? MensajeEnviarEmail { get; set; }

        public bool? Estado { get; set; }
    }
}
