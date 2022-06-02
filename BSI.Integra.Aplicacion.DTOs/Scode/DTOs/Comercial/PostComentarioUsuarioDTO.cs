using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PostComentarioUsuarioCompuestoDTO
    {
        public int Id { get; set; }
        public string IdUsuarioFacebook { get; set; }
        public string Nombres { get; set; }
        public int IdPersonal { get; set; }
        public bool TieneRespuesta { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Mensaje { get; set; }
        public string IdParent { get; set; }
        public string IdPostFacebook { get; set; }
        public string IdCommentFacebook { get; set; }
        public bool? ComentarioSinRpta { get; set; }
        public string PublicacionDescripcion { get; set; }
        public string PublicacionUrlImagen { get; set; }
        public string IdPrimerComentario { get; set; }

    }
}
