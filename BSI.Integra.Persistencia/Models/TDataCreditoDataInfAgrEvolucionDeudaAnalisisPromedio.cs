using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? CupoTotal { get; set; }
        public decimal? Saldo { get; set; }
        public string Porcentaje { get; set; }
        public decimal? Score { get; set; }
        public int? Calificacion { get; set; }
        public decimal? AperturaCuentas { get; set; }
        public decimal? CierreCuentas { get; set; }
        public string TotalAbiertas { get; set; }
        public string TotalCerradas { get; set; }
        public decimal? MoraMaxima { get; set; }
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
