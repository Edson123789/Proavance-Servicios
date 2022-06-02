using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfAgrEvolucionDeudaTrimestre
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? Cupototal { get; set; }
        public decimal? Saldo { get; set; }
        public string PorcentajeUso { get; set; }
        public decimal? Score { get; set; }
        public string Calificacion { get; set; }
        public string AperturaCuentas { get; set; }
        public string CierreCuentas { get; set; }
        public int? TotalAbiertas { get; set; }
        public int? TotalCerradas { get; set; }
        public string MoraMaxima { get; set; }
        public int? MesesMoraMaxima { get; set; }
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
