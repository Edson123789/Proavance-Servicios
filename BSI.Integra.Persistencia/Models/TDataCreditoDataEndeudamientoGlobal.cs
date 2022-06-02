using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TDataCreditoDataEndeudamientoGlobal
    {
        public int Id { get; set; }
        public int IdDataCreditoBusqueda { get; set; }
        public string Calificacion { get; set; }
        public string Fuente { get; set; }
        public string SaldoPendiente { get; set; }
        public string TipoCredito { get; set; }
        public string Moneda { get; set; }
        public string NumeroCreditos { get; set; }
        public string Independiente { get; set; }
        public DateTime? FechaReporte { get; set; }
        public string EntidadNombre { get; set; }
        public string EntidadNit { get; set; }
        public string EntidadSector { get; set; }
        public string GarantiaTipo { get; set; }
        public string GarantiaValor { get; set; }
        public string Llave { get; set; }
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
