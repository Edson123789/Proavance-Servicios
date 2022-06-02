using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RespuestaComentarioInstagramDTO
    {
        public string InstagramIdComentario { get; set; }
        public int IdInstagramUsuario { get; set; }
        public int IdInstagramPublicacion { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
        public int IdPersonal { get; set; }
    }
}
