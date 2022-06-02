using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteEncuestasInicialesDTO
    {
        public int Id { get; set; }
        public string IdPGeneral { get; set; }
        public string Programa { get; set; }
        public string IdPEspecifico { get; set; }
        public string Curso { get; set; }
        public string Docente { get; set; }
        public string Fecha { get; set; }
        public string CodMatricula { get; set; }
        public string IdAlumno { get; set; }
        public string Alumno { get; set; }
        public string Pregunta1 { get; set; }
        public string Pregunta2 { get; set; }
        public string Pregunta3 { get; set; }
        public string Pregunta4 { get; set; }
        public string Pregunta5 { get; set; }
        public string Pregunta6 { get; set; }
        public string Pregunta7 { get; set; }
    }

    public class ReporteEncuestasInicialesFiltroDTO
    {
        public List<int> Programa { get; set; }
        public List<int> Curso{ get; set; }
        public List<int> Docente { get; set; }
        public string Matricula { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
