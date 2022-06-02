using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CambiarEntregaDocumentosDTO
    {
        public int IdCriterioDocs { get; set; }
        public bool Ingresar { get; set; }
        public string usuario { get; set; }
    }
}
