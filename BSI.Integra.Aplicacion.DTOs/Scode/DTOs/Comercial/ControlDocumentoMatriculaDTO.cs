using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ControlDocumentoMatriculaDTO
    {
        public int IdControlDoc { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdCriterioDoc { get; set; }
        public string NombreDocumento { get; set; }
        public bool EstadoDocumento { get; set; }
    }
}
