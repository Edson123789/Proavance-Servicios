using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Servicios.SCode.DTOs
{
    public class ApiGraphFacebookResponseSubirListasDTO
    {
        public string audience_id { get; set; }
        public string session_id { get; set; }
        public int num_received { get; set; }
        public int num_invalid_entries { get; set; }
    }
}
