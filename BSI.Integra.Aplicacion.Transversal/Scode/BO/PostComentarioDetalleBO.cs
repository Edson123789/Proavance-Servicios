using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PostComentarioDetalleBO : BaseBO
    {
        public string IdCommentFacebook { get; set; }
        public string IdPostFacebook { get; set; }
        public string IdParent { get; set; }
        public string IdUsuarioFacebook { get; set; }
        public string Message { get; set; }
        public int? IdPersonal { get; set; }
        public bool? Tipo { get; set; }
        public bool? MensajeInicio { get; set; }

        public PostComentarioDetalleBO() { }
    }
}
