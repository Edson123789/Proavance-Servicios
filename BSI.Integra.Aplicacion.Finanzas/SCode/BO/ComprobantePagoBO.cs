using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
    public class ComprobantePagoBO : BaseBO
    {
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
        public decimal MontoNeto { get; set; }
        public decimal AjusteMontoBruto { get; set; }
        public decimal? OtraTazaContribucion { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EsDiferido { get; set; }
        public int? IdComprobantePagoEstado {get;set;}
        public DateTime? FechaVencimientoReprogramacion { get; set; }
        public List<ComprobantePagoPorFurBO> ComprobantePagoPorFur { get; set; }
    }

}
