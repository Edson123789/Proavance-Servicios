using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EncuestaFinalNuevaAulaDTO
    {
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string Fecha { get; set; }
        public string Docente { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string Respuesta { get; set; }

        public int IdPregunta { get; set; }
    }

    public class FiltroReporteEncuestaFinalNuevaAulaDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<int> ProgramaGeneral { get; set; }
        public List<int> ProgramaEspecifico { get; set; }
        public string CodigoMatricula { get; set; }
        public List<int> Docente { get; set; }
    }

    public class ReporteEncuestaFinalNuevaAulaDTO
    {
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string Fecha { get; set; }
        public string Docente { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string Pregunta1ServicioAtencionCoordinador { get; set; }
        public string Pregunta2PrecisionInformacion { get; set; }
        public string Pregunta3TiempoRespuestaConsultas { get; set; }
        public string Pregunta4CapacidadRespuestaSolicitud { get; set; }
        public string Pregunta5MecanismoEvaluacionCurso { get; set; }
        public string Pregunta6ForoConsultas { get; set; }
        public string Pregunta7NivelDificultadAutoevaluacion { get; set; }
        public string Pregunta8NivelExigenciaTarea { get; set; }
        public string Pregunta9PlataformaDigitalFacilitaCurso { get; set; }
        public string Pregunta10CalidadVideos { get; set; }
        public string Pregunta11MaterialAudiovisual { get; set; }
        public string Pregunta12MaterialesCurso { get; set; }
        public List<string> Pregunta13FactoresMotivacionMatricula { get; set; }
        public string Pregunta14FactorDecisivoMatricula { get; set; }
        public string Pregunta15FortalezaCurso { get; set; }
        public string Pregunta16DebilidadCurso { get; set; }
        public string Pregunta17RecomendacionPrograma { get; set; }
        public string Pregunta18VolverInscripcion { get; set; }
    }

    public class FiltroPgeneral
    {
        public List<int> Pgeneral { get; set; }
    }
}
