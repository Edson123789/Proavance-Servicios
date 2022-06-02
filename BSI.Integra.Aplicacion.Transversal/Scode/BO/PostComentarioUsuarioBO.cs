using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class PostComentarioUsuarioBO : BaseBO
    {
        public string IdUsuarioFacebook { get; set; }
        public string Nombres { get; set; }
        public int? IdPersonal { get; set; }
        public bool TieneRespuesta { get; set; }
        public string IdAreaCapacitacion { get; set; }

        public PostComentarioUsuarioBO() { }
    }
}
