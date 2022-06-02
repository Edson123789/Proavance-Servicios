using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoAsociadoDTO
    {
        public int IdDocumentos { get; set; }
        public string Nombre { get; set; }
        public int IdPlantillaPW { get; set; }
        public int EstadoFlujo { get; set; }
        public bool Asignado { get; set; }
        public int IdPGeneralDocumento { get; set; }
    }
}
