using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TContadorBicHistorico
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int DiasSinContactoManhana { get; set; }
        public int DiasSinContactoTarde { get; set; }
        public DateTime FechaActualizado { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
