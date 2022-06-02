using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PostComentarioUsuarioLogBO : BaseBO
    {
        public int? IdPostComentarioUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public PostComentarioUsuarioLogBO() { }
    }
}
