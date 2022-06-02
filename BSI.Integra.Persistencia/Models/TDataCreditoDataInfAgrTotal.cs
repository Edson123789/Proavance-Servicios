using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataInfAgrTotal
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string TipoMapeo { get; set; }
        public string CodigoTipo { get; set; }
        public string Tipo { get; set; }
        public string CalidadDeudor { get; set; }
        public string Participacion { get; set; }
        public string Cupo { get; set; }
        public string Saldo { get; set; }
        public string SaldoMora { get; set; }
        public string Cuota { get; set; }
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
