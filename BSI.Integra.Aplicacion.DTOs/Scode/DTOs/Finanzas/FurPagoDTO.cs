using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FurPagoDTO
    {
        public int Id { get; set; }
        public int? IdFur { get; set; }
        public int? IdComprobantePago { get; set; }
        public int NumeroPago { get; set; }
        public int IdMoneda { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroRecibo { get; set; }
        public int IdFormaPago { get; set; }
        public DateTime FechaCobroBanco { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaDolares { get; set; }
        public string Usuario { get; set; } 
        public bool IdCancelado { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }

    }
}
