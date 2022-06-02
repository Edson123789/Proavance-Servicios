using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CrepListaAlumnosDTO
    {
        public string CodigoProgramaEspecifico { get; set; }
        public string NombreProgramaEspecifico { get; set; }
        public string CodigoMatricula { get; set; }
        public int CodigoAlumno { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public DateTime FechaMatricula { get; set; }
        public string Estado { get; set; }
    }
}
