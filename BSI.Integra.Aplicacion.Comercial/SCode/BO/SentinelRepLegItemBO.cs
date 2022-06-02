using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class SentinelRepLegItemBO : BaseBO
    {
        public int? IdSentinel { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string RazonSocial { get; set; }
        public string Cargo { get; set; }
        public string SemaforoActual { get; set; }
		public Guid? IdMigracion { get; set; }
	}
}
