using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaPagoTarifario
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int? IdTarifario { get; set; }
        public int? IdTarifarioDetalle { get; set; }
        public string Concepto { get; set; }
        public string Moneda { get; set; }
        public string WebMoneda { get; set; }
        public decimal? Monto { get; set; }
        public bool? Cancelado { get; set; }
        public bool? Gestionado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime? FechaPago { get; set; }
    }
}
