using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PostComentarioDetalleDTO
    {
        public int Id { get; set; }
        public string IdUsuarioFacebook { get; set; }
        public int? IdPersonal { get; set; }
        public string Mensaje { get; set; }
        public bool Tipo { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string NombreUsuarioFB { get; set; }
        public string EmailAsesor { get; set; }
        public string IdCommentFacebook { get; set; }
        public string IdPostFacebook { get; set; }
        public string IdParent { get; set; }
        public string Nombre { get; set; }
        public string ProgramaGeneral { get; set; }
        public string CentroCosto { get; set; }
        public string CentroCosto2 { get; set; }
    }
}
