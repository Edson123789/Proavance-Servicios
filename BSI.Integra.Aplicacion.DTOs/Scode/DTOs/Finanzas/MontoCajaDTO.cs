using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs{
    public class MontoCajaDTO
    {
        public decimal NotaIngresoCaja { get; set; }
        public decimal ReciboEgresoCaja { get; set; }
        public decimal PorRendir { get; set; }
        public decimal SaldoCaja { get; set; }
    }
}
