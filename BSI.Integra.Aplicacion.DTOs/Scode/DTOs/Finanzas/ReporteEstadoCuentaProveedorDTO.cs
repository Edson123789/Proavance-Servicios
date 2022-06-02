using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteEstadoCuentaProveedorDTO
    {
        public string EmpresaSede  { get; set; }
        public string CodigoFur  { get; set; }
        public string RucProveedor  { get; set; }
        public string NombreProveedor  { get; set; }
        public string NumeroCuenta  { get; set; }
        public string DescripcionCuenta  { get; set; }
        public string  NumeroComprobante { get; set; }
        public string  NombreMoneda { get; set; }
        public decimal  MontoPagado { get; set; }
        public decimal MontoBoleta  { get; set; }
        public string Estado  { get; set; }
        public decimal  MontoPendiente { get; set; }
        public string NumeroCuentaCorriente  { get; set; }
        public string NumeroRecibo  { get; set; }
        public string TipoPago  { get; set; }
        public string FechaPagoBanco { get; set; }
        public string MesPagoBanco  { get; set; }
        public DateTime FechaEmisionComprobante  { get; set; }
        public DateTime FechaVencimientoComprobante  { get; set; }

    }
}
