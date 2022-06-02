using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoCronogramaDocumentoDTO
    {
        public int Id { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdMontoPago { get; set; }
        public double? PrecioDescuento { get; set; }
        public int? IdMoneda { get; set; }
    }
}
