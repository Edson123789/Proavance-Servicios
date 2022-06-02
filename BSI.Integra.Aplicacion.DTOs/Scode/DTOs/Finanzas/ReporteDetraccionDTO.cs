using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteDetraccionDTO
    {
        public string Empresa { get; set; }
        public string NroDocIdentidad { get; set; }
        public string NombreProveedor { get; set; }
        public string NumeroComprobante { get; set; }
        public string NombreMoneda { get; set; }
        public decimal MontoBruto { get; set; }
        public decimal MontoIgv { get; set; }
        public decimal MontoNeto { get; set; }
        public int? PorcentajeDetraccion { get; set; }
        public decimal MontoDetraccion { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime PeriodoTributario { get; set; }
    }
}
