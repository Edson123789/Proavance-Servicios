using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MessengerFacebookDTO
    {
        public string PSID { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Foto { get; set; }
        public string Mensaje { get; set; }
        public string MensajeBot { get; set; }
        public int Asesor { get; set; }

        public bool SonidoNuevoChat { get; set; }

        public bool Status { get; set; }
        public string ErrorMessage { get; set; }

        public string postbackPayload { get; set; }
        public bool Bot { get; set; }
        public bool enviarQuickTelefono { get; set; }
        public string RecipientId { get; set; }

    }
}
