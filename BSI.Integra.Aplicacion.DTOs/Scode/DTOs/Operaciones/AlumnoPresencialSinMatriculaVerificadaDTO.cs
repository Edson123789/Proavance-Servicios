using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoPresencialSinMatriculaVerificadaDTO
    {
        public string NombreCentroCosto { get; set; }
        public string NombreAlumno { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string UsuarioCoordinadorAcademico { get; set; }
        public string EstadoMatricula { get; set; }
        public int IdEstadoMatricula { get; set; }
        public string Genero { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
    }
}
