using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTokenPostulanteProcesoSeleccion
    {
        public int Id { get; set; }
        public int IdPostulanteProcesoSeleccion { get; set; }
        public string Token { get; set; }
        public string TokenHash { get; set; }
        public Guid GuidAccess { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public DateTime? FechaEnvioAccesos { get; set; }
    }
}
