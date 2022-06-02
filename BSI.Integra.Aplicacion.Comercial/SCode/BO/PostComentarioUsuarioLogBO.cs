using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class PostComentarioUsuarioLogBO : BaseEntity
    {
        public int? IdPostComentarioUsuario { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
