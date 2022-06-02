using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class IdentificadorCursoMoodlePorMatriculaComboDTO
    {
        public int IdCursoMoodle { get; set; }
        public int IdUsuarioMoodle { get; set; }
        public string NombreCurso { get; set; }
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
}
