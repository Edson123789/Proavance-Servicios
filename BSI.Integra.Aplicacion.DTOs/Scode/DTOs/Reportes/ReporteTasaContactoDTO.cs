using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteTasaContactoDTO
    {
        public int TotalLlamadas { get;set;}
        public int TotalLlamadasEjecutadas { get; set; }
        public int TotalLlamadasEjecutadasConLlamada { get; set; }

    }
}
