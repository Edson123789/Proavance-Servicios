using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoProgramaDTO
    {
        public List<DocumentoAsociadoDTO> DocumentosAsociados { get; set; }
        public List<DocumentoNoAsociadoDTO> DocumentosNoAsociados { get; set; }
    }
}
