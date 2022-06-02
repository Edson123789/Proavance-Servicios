using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostComentarioDetalle
    {
        public int Id { get; set; }
        public string IdCommentFacebook { get; set; }
        public string IdPostFacebook { get; set; }
        public string IdParent { get; set; }
        public string IdUsuarioFacebook { get; set; }
        public string Message { get; set; }
        public int? IdPersonal { get; set; }
        public bool? Tipo { get; set; }
        public bool? MensajeInicio { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
