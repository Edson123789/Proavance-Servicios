using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoEmailDTO
    {
        public int Id { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
    }
    public class RespuestaSMSOportunidadDTO
    {
        public int Resultado { get; set; }
    }
    public class AlumnoCuponDTO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public string CodigoCupon { get; set; }
    }
}
