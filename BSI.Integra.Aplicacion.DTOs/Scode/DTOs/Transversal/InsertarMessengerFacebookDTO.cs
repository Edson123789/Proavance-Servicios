using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InsertarMessengerFacebookDTO
    {
        public string PSID {get;set;}
        public string Mensaje {get;set;}
        public string Usuario {get;set;}
        public int? Asesor {get;set;}
    }
}
