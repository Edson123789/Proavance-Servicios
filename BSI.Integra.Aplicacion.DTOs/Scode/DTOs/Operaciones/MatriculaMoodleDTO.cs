using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaMoodleDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdAlumnoIntegra { get; set; }
        public int IdMatriculaMoodle { get; set; }
        public int IdUsuarioMoodle { get; set; }
        public int IdCursoMoodle { get; set; }
    }
}
