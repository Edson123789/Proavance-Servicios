using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TComprobantePago
    {
        public TComprobantePago()
        {
            TComprobantePagoPorFur = new HashSet<TComprobantePagoPorFur>();
        }

        public int Id { get; set; }
        public int IdSunatDocumento { get; set; }
        public int IdPais { get; set; }
        public string SerieComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public int IdMoneda { get; set; }
        public decimal MontoBruto { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdProveedor { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public decimal MontoNeto { get; set; }
        public decimal AjusteMontoBruto { get; set; }
        public int? IdComprobantePagoEstado { get; set; }
        public DateTime? FechaVencimientoReprogramacion { get; set; }
        public decimal? MontoInafecto { get; set; }
        public decimal? MontoIgv { get; set; }
        public decimal? PorcentajeIgv { get; set; }
        public decimal? OtraTazaContribucion { get; set; }

        public virtual ICollection<TComprobantePagoPorFur> TComprobantePagoPorFur { get; set; }
    }
}
