using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PostComentarioUsuarioDTO
    {
        public int Id { get; set; }
        public string IdUsuarioFacebook { get; set; }
        public string Nombres { get; set; }
        public int? IdPersonal { get; set; }
        public bool TieneRespuesta { get; set; }
        public string IdAreaCapacitacion { get; set; }
    }
}
