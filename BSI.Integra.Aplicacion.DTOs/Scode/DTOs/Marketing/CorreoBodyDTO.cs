using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class CorreoBodyDTO
    {
        public string EmailBody { get; set; }
        public List<CorreoArchivoAdjuntoDTO> ArchivosAdjuntos { get; set; }
        public CorreoBodyDTO() {
            ArchivosAdjuntos = new List<CorreoArchivoAdjuntoDTO>();
        }
    }
}
