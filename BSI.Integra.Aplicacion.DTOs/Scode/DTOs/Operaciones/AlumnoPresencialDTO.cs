using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoPresencialDTO
    {
        public int IdRaAlumno {get; set; }
        public string CodigoAlumno {get; set; }
        public string NombreAlumno {get; set; }
        public string NombreCentrocosto {get; set; }
        public string Nombre1 {get; set; }
        public string Nombre2 {get; set; }
        public string Email1 {get; set; }
        public string Email2 {get; set; }
        public string UsuarioCoordinadorAcademico {get; set; }
        public int IdEstadomatricula {get; set; }
        public string Estadomatricula {get; set; }
        public string Genero { get; set; }
    }
}
