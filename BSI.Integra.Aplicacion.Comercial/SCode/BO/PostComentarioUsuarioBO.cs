using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class PostComentarioUsuarioBO : BaseEntity
    {
        public string IdUsuario { get; set; }
        public string Nombres { get; set; }
        public int IdAsesor { get; set; }
        public bool Respuesta { get; set; }
        public bool? Tipo { get; set; }
        public string Area { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
