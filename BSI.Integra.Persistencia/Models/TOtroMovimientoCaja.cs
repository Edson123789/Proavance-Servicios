using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TOtroMovimientoCaja
    {
        public int Id { get; set; }
        public int IdSubTipoMovimientoCaja { get; set; }
        public int? IdAlumno { get; set; }
        public decimal Precio { get; set; }
        public int IdMoneda { get; set; }
        public DateTime FechaPago { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPlanContable { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int? IdFormaPago { get; set; }
        public string Observaciones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
