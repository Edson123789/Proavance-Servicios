using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.SCode.DTOs
{
    public class MoodleWebServiceCrearUsuarioDTO
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string auth { get; set; }
        public string country { get; set; }
        public string city { get; set; }
    }
}
