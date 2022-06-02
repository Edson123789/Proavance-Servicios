using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoComprobantePago2DTO
    {
        public ComprobantePagoDatosDTO ComprobantePago { get; set; }
        public string Usuario { get; set; }
        public int? IdFur { get; set; }

    }
}
