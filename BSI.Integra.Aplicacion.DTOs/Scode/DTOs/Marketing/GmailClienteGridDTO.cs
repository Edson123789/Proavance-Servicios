using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class GmailClienteGridDTO
    {
        public List<GmailClienteDTO> lista { get; set; }
        public int Total { get; set; }
    }
}
