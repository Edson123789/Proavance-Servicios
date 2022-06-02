using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComprobantePagoDTO
    {
        public int Id { get; set; }
        public int? IdDocumentoPago { get; set; }
        public string NombreDocumento { get; set; }
        public string SerieComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public decimal Monto { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdIgv { get; set; }
        public int? ValorIGV { get; set; }
        public int? IdRetencion { get; set; }
        public int? ValorRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? ValorDetraccion { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string CodigoFur { get; set; }
        public decimal MontoAfecto { get; set; }
        public decimal? MontoInafecto { get; set; }
        public decimal? OtraTazaContribucion { get; set; }
        public decimal AjusteMontoBruto { get; set; }

    }
}
