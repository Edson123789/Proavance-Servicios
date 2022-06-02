using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteDePrestamoDTO
    {
        public int NumeroCuota { get; set; }
        public DateTime FechaVencimientoCuota { get; set; }
        public decimal CapitalCuota { get; set; }
        public decimal InteresCuota { get; set; }
        public decimal TotalCuota { get; set; }
        public string NombreMoneda { get; set; }
    }
}
