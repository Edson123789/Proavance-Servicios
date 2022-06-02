using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoMatriculaCursoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string Alumno { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }
    }
}
