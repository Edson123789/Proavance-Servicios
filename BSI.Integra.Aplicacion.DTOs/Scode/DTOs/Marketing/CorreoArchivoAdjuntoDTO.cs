using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CorreoArchivoAdjuntoDTO
    {
        public int IdCorreo { get; set; }
        public string NombreArchivo { get; set; }
        public string UrlArchivoRepositorio { get; set; }
    }
}
