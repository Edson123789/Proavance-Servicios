using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoCronogramaDetalleDTO
    {
        public int Id { get; set; }
        public int NumeroCuota { get; set; }
        public string CuotaDescripcion { get; set; }
        public double MontoCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public double MontoCuotaDescuento { get; set; }
        public bool Pagado { get; set; }
        public bool Matricula { get; set; }
        public string Cronograma { get; set; }
        public string RowVersion { get; set; }
    }
}
