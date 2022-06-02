using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCompromisoAlumno
    {
        public int Id { get; set; }
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public DateTime FechaCompromiso { get; set; }
        public DateTime FechaGeneracionCompromiso { get; set; }
        public decimal Monto { get; set; }
        public int? IdMoneda { get; set; }
        public int Version { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
