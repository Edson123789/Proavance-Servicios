using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class InstagramComentarioPersonalDTO
    {
        public int IdInstagramPublicacion { get; set; }
        public string InstagramIdComentario { get; set; }
        public DateTime FechaInteraccion { get; set; }
        public string Texto { get; set; }
        public string InstagramId { get; set; }
        public int IdInstagramUsuario { get; set; }
        public string Usuario { get; set; }
        public string Subtitulo { get; set; }
        public string UrlMedia { get; set; }
        public string TipoMedia { get; set; }
    }
}
