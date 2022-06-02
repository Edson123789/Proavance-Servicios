using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RelacionProgramasDTO
    {
        public List<PGeneralFiltroDTO> CursosNoRelacionados { get; set; }
        public List<CursoRelacionadoProgramaDTO> CursosRelacionados { get; set; }
    }
}
