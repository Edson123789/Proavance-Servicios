using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCronogramaPagoDetalleFinalCierre
    {
        public int Id { get; set; }
        public int IdPeriodoCorte { get; set; }
        public string PeriodoNombre { get; set; }
        public decimal MontoProyectado { get; set; }
        public decimal MontoPagado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
