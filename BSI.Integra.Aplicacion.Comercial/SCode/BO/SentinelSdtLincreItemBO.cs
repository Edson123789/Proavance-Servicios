using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SentinelSdtLincreItemBO : BaseBO
    {
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string CnsEntNomRazLn { get; set; }
        public string TipoCuenta { get; set; }
        public decimal? LineaCredito { get; set; }
        public decimal? LineaCreditoNoUtil { get; set; }
        public decimal? LineaUtil { get; set; }
		public Guid? IdMigracion { get; set; }
	}
}
