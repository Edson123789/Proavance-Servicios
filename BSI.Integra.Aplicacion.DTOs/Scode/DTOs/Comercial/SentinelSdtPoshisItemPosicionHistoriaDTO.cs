using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SentinelSdtPoshisItemPosicionHistoriaDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public DateTime FechaProceso { get; set; }
        public string SemanaActual { get; set; }
        public decimal? Score { get; set; }
        public int? CodigoVariacion { get; set; }
        public int? NumeroEntidades { get; set; }
        public decimal? DeudaTotal { get; set; }
        public decimal? ProgresoRegistro { get; set; }
        public decimal? DocImpuesto { get; set; }
        public decimal? DeudaTributaria { get; set; }
        public string DeudaLab { get; set; }
        public int? CuentaCorriente { get; set; }
        public int? TarjetaCredito { get; set; }
        public int? ReporteNegativo { get; set; }
        public decimal? PorcentajeCalificacion { get; set; }
        public int? PeorCalificacion { get; set; }
    }
}
