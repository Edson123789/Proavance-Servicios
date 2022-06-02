using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoMatriculaDTO
    {
        public int CodigoAlumno { get; set; }
        public string CodigoProgramaEspecifico { get; set; }
        public string NombreProgramaEspecifico { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public string FechaMatricula { get; set; }
        public string Estado{ get; set; }

    }
}
