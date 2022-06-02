using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteEncuestasIntermediasDTO
    {
        public string Programa { get; set; }
        public string Curso { get; set; }
        public string Docente { get; set; }
        public string Fecha { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string Pregunta1 { get; set; }
        public string Pregunta2 { get; set; }
        public string Pregunta3 { get; set; }
        public string Pregunta4 { get; set; }
        public string Pregunta5 { get; set; }
        public string Pregunta6 { get; set; }
        public string Pregunta7 { get; set; }
        public string Pregunta8 { get; set; }
        public string Pregunta9 { get; set; }
        public string Pregunta10 { get; set; }
        public string Pregunta11 { get; set; }
        public string Pregunta12 { get; set; }
        public string Pregunta13 { get; set; }
        public string Pregunta14 { get; set; }
        public string Pregunta15 { get; set; }
        public string Pregunta16 { get; set; }
        public string Pregunta17 { get; set; }
        public string Pregunta18 { get; set; }

    }
    public class ReporteEncuestasIntermediasFiltroDTO
    {
        public List<int> Programa { get; set; }
        public List<int> Docente { get; set; }
        public List<int> Curso { get; set; }
        public string CodigoMatricula { get; set; }

        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }

    }
    public class FiltroPGeneral
    {
        public List<int> Pgeneral { get; set; }
    }

}
