using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PersonalAlumnoDTO
    {
        public int IdPersonal { get; set; }
        public int IdAlumno { get; set; }
    }

    public class PersonalNumeroMinimoChatDTO
    {
        public int IdPersonal { get; set; }
        public int NumeroChats { get; set; }
    }

    public class ValidarOportunidadWhatsAppDTO
    {
        public int IdPersonal { get; set; }
        public int IdPgeneral { get; set; }
        public string FaseOportunidad { get; set; }
    }
}
