using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PagoMasivoDatosDTO
    {
        public int? Id { get; set; }
        public string Codigo { get; set; }
        public decimal Monto { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public decimal MontoAmortizar { get; set; }
    }
}
