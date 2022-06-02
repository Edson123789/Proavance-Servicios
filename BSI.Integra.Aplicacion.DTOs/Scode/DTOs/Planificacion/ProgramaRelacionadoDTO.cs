using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProgramaRelacionadoDTO
    {
        public List<CursoRelacionadoProgramaDTO> Cursos { get; set; } 
        public string Usuario { get; set; }
        public int IdPGeneral { get; set; }
    }
}
