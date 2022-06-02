using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfAgrResumenSaldo
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public decimal? SaldoTotalEnMora { get; set; }
        public decimal? SaldoM30 { get; set; }
        public decimal? SaldoM60 { get; set; }
        public decimal? SaldoM90 { get; set; }
        public decimal? CuotaMensual { get; set; }
        public decimal? SaldoCreditoMasAlto { get; set; }
        public decimal? SaldoTotal { get; set; }
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
