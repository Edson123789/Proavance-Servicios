using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostComentarioUsuario
    {
        public int Id { get; set; }
        public string IdUsuarioFacebook { get; set; }
        public string Nombres { get; set; }
        public int? IdPersonal { get; set; }
        public bool TieneRespuesta { get; set; }
        public string IdAreaCapacitacion { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
