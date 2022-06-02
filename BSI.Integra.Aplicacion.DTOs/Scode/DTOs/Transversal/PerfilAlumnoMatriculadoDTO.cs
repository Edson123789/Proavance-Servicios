using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PerfilAlumnoMatriculadoDTO
    {
        public int IdAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Alumno { get; set; }
        public string AreaFormacion { get; set; }
        public string AreaTrabajo { get; set; }
        public string Industria { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
    }
}
