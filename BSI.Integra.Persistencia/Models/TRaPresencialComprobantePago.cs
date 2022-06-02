using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TRaPresencialComprobantePago
    {
        public int Id { get; set; }
        public int? IdRaSesion { get; set; }
        public int? IdMoneda { get; set; }
        public int IdRaTipoComprobantePago { get; set; }
        public decimal? Monto { get; set; }
        public string Observacion { get; set; }
        public int? IdMonedaViatico { get; set; }
        public decimal? MontoViatico { get; set; }
        public decimal? TipoCambio { get; set; }
        public string NumeroComprobante { get; set; }
        public int? IdFur { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
