using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnosSentinelLineasCreditoDTO
    {
        public int Id { get; set; }
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string CnsEntNomRazLn { get; set; }
        public string TipoCuenta { get; set; }
        public decimal? LineaCredito { get; set; }
        public decimal? LineaCreditoNoUtil { get; set; }
        public decimal? LineaUtil { get; set; }
    }
}
