using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComprobanteDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string Ruc { get; set; }
        public string Proveedor { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public int IdSunatDocumento { get; set; }
        public string SunatDocumento { get; set; }
        public string Comprobante { get; set; }
        public decimal MontoBruto { get; set; }
        public int IdMoneda { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}
