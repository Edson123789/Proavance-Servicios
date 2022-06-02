using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSentinelSdtPoshisItem
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime? FechaProceso { get; set; }
        public string SemanaActual { get; set; }
        public string DescripcionSemaforo { get; set; }
        public decimal? Score { get; set; }
        public int? CodigoVariacion { get; set; }
        public string DescripcionVariacion { get; set; }
        public int? NumeroEntidades { get; set; }
        public decimal? DeudaTotal { get; set; }
        public decimal? PorcentajeCalificacion { get; set; }
        public int? PeorCalificacion { get; set; }
        public string PeroCalificacionDescripcion { get; set; }
        public decimal? MontoSbs { get; set; }
        public decimal? ProgresoRegistro { get; set; }
        public decimal? DocImpuesto { get; set; }
        public decimal? DeudaTributaria { get; set; }
        public decimal? Afp { get; set; }
        public int? TarjetaCredito { get; set; }
        public int? CuentaCorriente { get; set; }
        public int? ReporteNegativo { get; set; }
        public decimal? DeudaDirecta { get; set; }
        public decimal? DeudaIndirecta { get; set; }
        public decimal? LineaCreditoNoUtilizada { get; set; }
        public decimal? DeudaCastigada { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual TSentinel IdSentinelNavigation { get; set; }
    }
}
