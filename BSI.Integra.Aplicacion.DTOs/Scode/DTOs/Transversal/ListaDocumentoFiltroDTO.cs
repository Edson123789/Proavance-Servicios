using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListaDocumentoFiltroDTO
    {
        public int IdDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public bool EsActivo { get; set; }
    }
}
