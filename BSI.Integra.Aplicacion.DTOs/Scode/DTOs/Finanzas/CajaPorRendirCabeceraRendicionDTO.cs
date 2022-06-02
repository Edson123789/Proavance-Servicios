using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CajaPorRendirCabeceraRendicionDTO
    {
        public int IdCajaPorRendirCabecera { get; set; }
        public decimal MontoSolicitado { get; set; }
        public decimal MontoPendiente { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public int NumeroSolicitudes { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CodigoPR { get; set; }
        public int IdCaja { get; set; }
        public string CodigoCaja { get; set; }
        
    }
}
