using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ComprobantePagoInsercionDTO
    {
        public int Id { get; set; }
        public int IdSunatDocumento { get; set; }
        public int IdPais { get; set; }
        public int? IdProveedor { get; set; }
        public string SerieComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public int IdMoneda { get; set; }
        public decimal MontoBruto { get; set; }
        public decimal? MontoInafecto { get; set; }
        public decimal? PorcentajeIgv { get; set; }
        public decimal? MontoIgv { get; set; }
        public decimal AjusteMontoBruto { get; set; }
        public decimal MontoNeto { get; set; }
        public decimal? OtraTazaContribucion { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public string Usuario { get; set; }
    }
}
