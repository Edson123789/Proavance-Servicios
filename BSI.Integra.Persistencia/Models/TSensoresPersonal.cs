using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TSensoresPersonal
    {
        public int Id { get; set; }
        public int? IdPersonal { get; set; }
        public int? VelocidadInternet { get; set; }
        public int? Procesador { get; set; }
        public int? Ram { get; set; }
        public int? PingIntegra { get; set; }
        public int? PingLan { get; set; }
        public string PingWebPhone { get; set; }
        public int? PingCentral { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
