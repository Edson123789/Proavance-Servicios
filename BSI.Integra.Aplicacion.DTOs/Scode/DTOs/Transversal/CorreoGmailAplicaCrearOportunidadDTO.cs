using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CorreoGmailAplicaCrearOportunidadDTO
    {
        public int IdCorreoGmail { get; set; }
        public bool AplicaCrearOportunidad { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class CorreoGmailDesuscribirDTO
    {
        public int IdCorreoGmail { get; set; }
        public bool EsMarcadoDesuscrito { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class CorreoGmailDescartarDTO
    {
        public int IdCorreoGmail { get; set; }
        public bool EsDescartado { get; set; }
        public string NombreUsuario { get; set; }
    }
}
