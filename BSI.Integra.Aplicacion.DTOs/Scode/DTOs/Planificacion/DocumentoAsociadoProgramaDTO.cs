using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoAsociadoProgramaDTO
    {
        public List<DocumentoAsociadoDTO> Documentos { get; set; }
        public int IdPGeneral { get; set; }
        public string Usuario { get; set; }

    }
}
