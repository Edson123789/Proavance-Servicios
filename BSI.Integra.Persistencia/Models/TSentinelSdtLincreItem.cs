using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSentinelSdtLincreItem
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
