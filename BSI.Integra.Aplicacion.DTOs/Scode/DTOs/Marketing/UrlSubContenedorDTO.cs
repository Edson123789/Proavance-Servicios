using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class UrlSubContenedorDTO
    {
        public int Id { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string RutaCompleta { get; set; }
        public string RutaBlob { get; set; }
    }
}
