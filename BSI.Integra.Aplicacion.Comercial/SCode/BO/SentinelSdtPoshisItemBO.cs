using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SentinelSdtPoshisItemBO : BaseBO
    {
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaProceso { get; set; }
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
		public Guid? IdMigracion { get; set; }
	}
}
