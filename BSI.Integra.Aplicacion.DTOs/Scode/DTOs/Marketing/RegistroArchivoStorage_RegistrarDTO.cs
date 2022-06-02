using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistroArchivoStorage_RegistrarDTO
    {
        public int IdUrlSubContenedor { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreUsuario { get; set; }
        public string Ruta { get; set; }
    }
}
