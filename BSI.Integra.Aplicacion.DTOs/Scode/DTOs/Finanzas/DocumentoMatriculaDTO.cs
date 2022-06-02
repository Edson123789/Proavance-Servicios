using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DocumentoMatriculaDTO
    {
        public string CodigoMatricula { get; set; }
        public int IdCriterioDocs { get; set; }
        public string NombreDocumento { get; set; }
        public bool Estado { get; set; }
        public int EstadoEntero { get; set; }
    }
}
