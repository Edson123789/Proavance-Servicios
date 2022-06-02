using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PagoMasivoDTO
    {
        public int Id { get; set; }
        public int IdMoneda { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroRecibo { get; set; }
        public int IdFormaPago { get; set; }
        public DateTime FechaCobroBanco { get; set; }        
        public decimal PrecioTotalBloque { get; set; }
    }
}
