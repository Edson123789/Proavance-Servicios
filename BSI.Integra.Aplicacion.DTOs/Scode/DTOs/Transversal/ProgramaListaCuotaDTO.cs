using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaListaCuotaDTO
    {
        public DateTime? FechaVencimiento { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Mora { get; set; }
        public int? NroCuota { get; set; }
        public string Moneda { get; set; }
        public bool? Cancelado { get; set; }
        public decimal? MontoCuotaDescuento { get; set; }
    }
}
