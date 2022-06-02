using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaDetalleCambio
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int IdCronogramaCabeceraCambio { get; set; }
        public int NroCuota { get; set; }
        public int NroSubcuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public decimal TipoCambio { get; set; }
        public string Moneda { get; set; }
        public int Version { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
