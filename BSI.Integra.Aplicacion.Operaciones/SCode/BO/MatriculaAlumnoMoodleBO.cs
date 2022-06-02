using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class MatriculaAlumnoMoodleBO : BaseBO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaMoodle { get; set; }
        public int IdUsuarioMoodle { get; set; }
        public int IdCursoMoodle { get; set; }
        public int? IdMigracion { get; set; }
    }
}
