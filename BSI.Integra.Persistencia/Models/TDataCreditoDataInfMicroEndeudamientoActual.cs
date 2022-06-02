using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfMicroEndeudamientoActual
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string SectorCodigoSector { get; set; }
        public string TipoCuenta { get; set; }
        public string TipoUsuario { get; set; }
        public string EstadoActual { get; set; }
        public string Calificacion { get; set; }
        public decimal? ValorInicial { get; set; }
        public decimal? SaldoActual { get; set; }
        public decimal? SaldoMora { get; set; }
        public decimal? CuotaMes { get; set; }
        public bool? ComportamientoNegativo { get; set; }
        public decimal? TotalDeudaCarteras { get; set; }
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
