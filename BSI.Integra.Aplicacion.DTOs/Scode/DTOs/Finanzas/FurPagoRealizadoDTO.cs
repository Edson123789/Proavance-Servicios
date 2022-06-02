using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FurPagoRealizadoDTO
    {
        public int Id { get; set; }
        public int? IdComprobantePagoPorFur { get; set; }
        public string NombreComprobantePagoPorFur { get; set; }
        public int NumeroPago { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroRecibo { get; set; }
        public int IdFormaPago { get; set; }
        public string NombreFormaPago { get; set; }
        public DateTime FechaCobroBanco { get; set; }
        public decimal PrecioTotalMonedaOrigen { get; set; }
        public decimal PrecioTotalMonedaDolares { get; set; }
        public bool IdCancelado { get; set; }
        public string NombreCancelado { get; set; }
    }
}
