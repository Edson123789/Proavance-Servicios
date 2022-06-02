using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaDocumentosDTO
    {
        public int Id { get; set; }
        public string NombreDocumento { get; set; }
        public bool Habilitado { get; set; }
        public string Url { get; set; }
        public byte[] DocumentoByte { get; set; }
        public string Mensaje { get; set; }
        public string MensajeDetalle { get; set; }
        public List<AlertDTO> ListadoAlertas { get; set;}
    }
}
