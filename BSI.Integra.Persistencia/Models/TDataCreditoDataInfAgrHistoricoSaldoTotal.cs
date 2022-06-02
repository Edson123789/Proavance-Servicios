using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfAgrHistoricoSaldoTotal
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public int? TotalCuentas { get; set; }
        public int? CuentasConsideradas { get; set; }
        public decimal? Saldo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TDataCreditoBusqueda IdDataCreditoBusquedaNavigation { get; set; }
    }
}
