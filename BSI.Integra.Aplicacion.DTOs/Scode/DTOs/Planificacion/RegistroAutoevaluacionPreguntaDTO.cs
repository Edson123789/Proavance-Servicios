using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RegistroAutoevaluacionPreguntaDTO
    {
        public int Id { get; set; }
        public int IdAutoevaluacionOnline { get; set; }
        public int IdPreguntaTipo { get; set; }
        public string Descripcion { get; set; }
        public int Tiempo { get; set; }
        public int NumeroIntento { get; set; }
        public bool RespuestaAleatoria { get; set; }
        public int? Puntaje { get; set; }

        public List<RegistroAutoevaluacionRespuestaDTO> listaRespuestasAutoEvaluacion { get; set; }
        public List<RegistroAutoevaluacionIntentoDTO> listaIntentoAutoevaluacion { get; set; }
    }

    public class RegistroAutoevaluacionRespuestaDTO
    {
        public int Id { get; set; }
        public int IdAutoevaluacionPregunta { get; set; }
        public int Orden { get; set; }
        public string Descripcion { get; set; }
        public bool Correcta { get; set; }
        public string Feedback { get; set; }
    }

    public class RegistroAutoevaluacionIntentoDTO
    {
        public int Id { get; set; }
        public int IdAutoevaluacionPregunta { get; set; }
        public int Intento { get; set; }
        public decimal Puntaje { get; set; }

    }


    public class ImportarPreguntasAutoevaluacionDTO
    {
        public string PREGUNTA { get; set; }
        public int TIPOPREGUNTA { get; set; }
        public int TIEMPOLIMITE { get; set; }
        public bool RESPUESTAALEATORIA { get; set; }
        public int INTENTOABIERTO { get; set; }
        public int PUNTAJEABIERTO { get; set; }
        public int INTENTOCERRADA { get; set; }
        public int PUNTAJECERRADA { get; set; }
        public int RESPUESTAORDEN { get; set; }
        public string RESPUESTADESCRIPCION { get; set; }
        public bool RESPUESTACORRECTA { get; set; }
        public string RESPUESTAFEEDBACK { get; set; }

    }

}
