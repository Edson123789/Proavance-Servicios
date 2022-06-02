using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoDatosComprobantePagoDTO
    {
        public int IdComprobantePago { get; set; }
        public decimal Monto { get; set; }
        public string Usuario { get; set; }
        public int IdFur { get; set; }

    }
}
