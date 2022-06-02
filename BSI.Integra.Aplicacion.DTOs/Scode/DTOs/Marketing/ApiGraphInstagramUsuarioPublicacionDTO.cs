using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ApiGraphInstagramUsuarioPublicacionDTO
    {
        public string username { get; set; }
        public DateTime timestamp { get; set; }
        public ApiGraphInstagramPublicacionDTO media { get; set; }
    }
}
