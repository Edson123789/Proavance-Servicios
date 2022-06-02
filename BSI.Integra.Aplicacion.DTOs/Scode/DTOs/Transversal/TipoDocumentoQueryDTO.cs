using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal
{
    public class TipoDocumentoQueryDTO
    {
        public int Id { get; set; }
        public int ModalidadCurso { get; set; }
        public int EstadoMatricula { get; set; }
        public int SubEstadoMatricula { get; set; }
        public int OperadorComparacion { get; set; }
        public bool TieneDeuda { get; set; }
    }
}
