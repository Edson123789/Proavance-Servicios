using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GmailClienteDTO
    {
        public int Id { get; set; }
        public int? IdAsesor { get; set; }
        public string EmailAsesor { get; set; }
        public string PasswordCorreo { get; set; }
        public string NombreAsesor { get; set; }
        public string IdClient { get; set; }
        public string ClientSecret { get; set; }
        public string Usuario { get; set; }
    }
}
