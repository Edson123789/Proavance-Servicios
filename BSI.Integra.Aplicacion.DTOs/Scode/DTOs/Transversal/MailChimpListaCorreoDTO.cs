using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MailChimpListaCorreoDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool ExisteEnMailChimp { get; set; }
    }
}
