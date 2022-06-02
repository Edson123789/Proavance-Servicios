using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class PostComentarioDetalleBO : BaseEntity
    {
        public string IdCommentFacebook { get; set; }
        public string IdPostFacebook { get; set; }
        public string IdParent { get; set; }
        public string IdUsuarioFacebook { get; set; }
        public string Message { get; set; }
        public int? IdPersonal { get; set; }
        public bool? Tipo { get; set; }
        public bool? MensajeInicio { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
