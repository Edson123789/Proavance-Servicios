using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BandejaCorreoDTO
    {
        public List<CorreoDTO> ListaCorreos { get; set; }
        public int TotalEnviados { get; set; }
        public BandejaCorreoDTO() {
            ListaCorreos = new List<CorreoDTO>();
        }
    }

    public class BandejaCorreoGmailDTO
    {
        public List<ResumenCorreoGmailDTO> ListaCorreoGmail { get; set; }
        public int TotalCorreoGmail { get; set; }
        public BandejaCorreoGmailDTO() {
            ListaCorreoGmail = new List<ResumenCorreoGmailDTO>();
        }
    }

    public class ListaCorreosPersonaDTO
    {
        public List<gmailCredenciales> ListaCorreos { get; set; }
        public bool Errores { get; set; }
        public ListaCorreosPersonaDTO() {
            ListaCorreos = new List<gmailCredenciales>();
        }
    }

    public class gmailCredenciales
    {
        public string Email { get; set; }
        public string Clave { get; set; }
    }

    public class ListaCorreosGrupoBO
    {
        public string ListaCorreos { get; set; }
        public int TotalCorreos { get; set; }
        public bool Errores { get; set; }
    }
}
