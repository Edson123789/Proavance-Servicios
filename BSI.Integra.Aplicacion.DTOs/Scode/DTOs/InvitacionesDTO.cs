using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InvitacionesDTO
    {
        public string email { get; set; }
        public string displayName { get; set; }
        public bool coHost { get; set; }



    }

    public class CabeceraInvitacionDTO
    {
        public string title { get; set; }
        public string password { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string timezone { get; set; }
        public  List<InvitacionesDTO> invitees { get; set; }

    }
    public class ResultadoWebexDTO
    {
        public string UrlWebex { get; set; }
        public int Cuenta { get; set; }
        

    }
}
