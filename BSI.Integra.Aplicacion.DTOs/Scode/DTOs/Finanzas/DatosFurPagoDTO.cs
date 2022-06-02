using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosFurPagoDTO
    {
        public int NumeroPago { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroRecibo { get; set; }
        public int IdFormaPago { get; set; }
        public string FechaCobroBanco { get; set; }
    }
}
