using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfMicroEvolucionDeuda
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string CodigoSector { get; set; }
        public string NombreSector { get; set; }
        public string TipoCuenta { get; set; }
        public string Trimestre { get; set; }
        public string Num { get; set; }
        public decimal? CupoInicial { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? SaldoMora { get; set; }
        public decimal? Cuota { get; set; }
        public decimal? PorcentajeDeuda { get; set; }
        public string CodigoMenorCalificacion { get; set; }
        public string TextoMenorCalificacion { get; set; }
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
