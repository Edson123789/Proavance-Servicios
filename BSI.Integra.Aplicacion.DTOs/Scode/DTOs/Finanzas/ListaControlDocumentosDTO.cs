using BSI.Integra.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas
{
    public class ListaControlDocumentosDTO
    {
        public List<CambiarEntregaDocumentosDTO> ListaDocumentos { get; set; }
        public string matricula { get; set; }
    }
}
