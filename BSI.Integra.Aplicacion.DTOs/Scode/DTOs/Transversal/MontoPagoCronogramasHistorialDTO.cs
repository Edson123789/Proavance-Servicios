using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoCronogramasHistorialDTO
    {
        public string CodigoMatricula { get; set; }
        public string Nombre { get; set; }
        public int NroCuota { get; set; }
        public string MonedaPago { get; set; }
        public Decimal MontoPagado { get; set; }
        public Decimal Mora { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? Fechapago { get; set; }
        public bool Cancelado { get; set; }
        public DateTime? FechaMatricula { get; set; }
    }
}
