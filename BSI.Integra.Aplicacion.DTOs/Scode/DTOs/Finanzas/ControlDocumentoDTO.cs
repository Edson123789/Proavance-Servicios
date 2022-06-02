using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ControlDocumentoDTO
    {

        public int IdControlDoc { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public bool EstadoDocumento { get; set; }
        public int IdCriterioDoc { get; set; }
        public string NombreUsuario { get; set; }
    }
}
