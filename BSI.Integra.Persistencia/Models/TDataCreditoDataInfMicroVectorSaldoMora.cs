using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfMicroVectorSaldoMora
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public bool? PoseeSectorCooperativo { get; set; }
        public bool? PoseeSectorFinanciero { get; set; }
        public bool? PoseeSectorReal { get; set; }
        public bool? PoseeSectorTelcos { get; set; }
        public DateTime? Fecha { get; set; }
        public int? TotalCuentasMora { get; set; }
        public decimal? SaldoDeudaTotalMora { get; set; }
        public decimal? SaldoDeudaTotal { get; set; }
        public string MorasMaxSectorFinanciero { get; set; }
        public string MorasMaxSectorReal { get; set; }
        public string MorasMaxSectorTelcos { get; set; }
        public string MorasMaximas { get; set; }
        public int? NumCreditos30 { get; set; }
        public int? NumCreditosMayorIgual60 { get; set; }
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
