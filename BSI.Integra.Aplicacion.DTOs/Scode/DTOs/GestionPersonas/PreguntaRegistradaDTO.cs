using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PreguntaRegistradaDTO
	{
		public int Id { get; set; }
		public string Enunciado { get; set; }
		public int IdTipoRespuesta { get; set; }
		public int? IdPreguntaTipo { get; set; }
		public int? MinutosPorPregunta { get; set; }
		public bool? RespuestaAleatoria { get; set; }
		public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
		public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
		public bool? MostrarFeedbackInmediato { get; set; }
		public bool? MostrarFeedbackPorPregunta { get; set; }
		public int? NumeroMaximoIntento { get; set; }
		public bool? ActivarFeedbackMaximoIntento { get; set; }
		public string MensajeFeedbackIntento { get; set; }
		public int? IdExamen { get; set; }
		public string ComponenteExamen { get; set; }
		public int? IdTipoRespuestaCalificacion { get; set; }
		public int? FactorRespuesta { get; set; }
		public int? IdPreguntaCategoria { get; set; }
	}

	public class CompuestoPreguntaDTO
	{
		public PreguntaEvaluacionDTO Pregunta { get; set; }
		public ExamenPreguntaDTO Examen { get; set; }
		public PreguntaIntentoDTO PreguntaIntento { get; set; }
		public List<RespuestaPreguntaDTO> RespuestaPregunta { get; set; }
		public string Usuario { get; set; }
	}
	public class PreguntaEvaluacionDTO
	{
		public int Id { get; set; }
		public bool ActivarFeedBackRespuestaCorrecta { get; set; }
		public bool ActivarFeedBackRespuestaIncorrecta { get; set; }
		public string Enunciado { get; set; }
		public int? IdPreguntaTipo { get; set; }
		public int IdTipoRespuesta { get; set; }
		public int? MinutosPorPregunta { get; set; }
		public bool MostrarFeedbackInmediato { get; set; }
		public bool MostrarFeedbackPorPregunta { get; set; }
		public bool RespuestaAleatoria { get; set; }
		public int? IdTipoRespuestaCalificacion { get; set; }
		public int? FactorRespuesta { get; set; }
		public int? IdPreguntaCategoria { get; set; }
	}

	public class ExamenPreguntaDTO
	{
		public List<int> ListaExamen { get; set; }
	}

	public class PreguntaIntentoDTO
	{
		public bool ActivarFeedbackMaximoIntento { get; set; }
		public string MensajeFeedbackIntento { get; set; }
		public int? NumeroMaximoIntento { get; set; }
		public List<PreguntaIntentoDetalleDTO> PreguntaIntentoDetalle { get; set; }
	}

	public class PreguntaIntentoDetalleDTO
	{
		public int Id { get; set; }
		public int PorcentajeCalificacion { get; set; }
	}


	public class PreguntaAgrupadaDTO
	{
		public int Id { get; set; }
		public string Enunciado { get; set; }
		public int IdTipoRespuesta { get; set; }
		public int? IdPreguntaTipo { get; set; }
		public int? MinutosPorPregunta { get; set; }
		public bool? RespuestaAleatoria { get; set; }
		public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
		public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
		public bool? MostrarFeedbackInmediato { get; set; }
		public bool? MostrarFeedbackPorPregunta { get; set; }
		public int? NumeroMaximoIntento { get; set; }
		public bool? ActivarFeedbackMaximoIntento { get; set; }
		public string MensajeFeedbackIntento { get; set; }
		public List<int?> ListaExamen { get; set; }
		public List<string> ComponenteExamen { get; set; }
		public int? IdTipoRespuestaCalificacion { get; set; }
		public int? FactorRespuesta { get; set; }
		public int? IdPreguntaCategoria { get; set; }
	}

	//================================================

	public class PreguntaProgramaCapacitacionDTO
	{
		public int Id { get; set; }
		public bool ActivarFeedBackRespuestaCorrecta { get; set; }
		public bool ActivarFeedBackRespuestaIncorrecta { get; set; }
		public string Enunciado { get; set; }
		public int? IdPreguntaTipo { get; set; }
		public int IdTipoRespuesta { get; set; }
		public int? MinutosPorPregunta { get; set; }
		public bool MostrarFeedbackInmediato { get; set; }
		public bool MostrarFeedbackPorPregunta { get; set; }
		public bool RespuestaAleatoria { get; set; }
		public int? IdTipoRespuestaCalificacion { get; set; }
		public int? FactorRespuesta { get; set; }
		public int IdPGeneral { get; set; }
		public int? IdPEspecifico { get; set; }
		public int? IdCapitulo { get; set; }
		public int? IdSesion { get; set; }
		public string GrupoPregunta { get; set; }
		public int? IdTipoMarcador { get; set; }
		public decimal? ValorMarcador { get; set; }
		public int? OrdenPreguntaGrupo { get; set; }
	}
	public class CompuestoPreguntaProgramaCapacitacionDTO
	{
		public PreguntaProgramaCapacitacionDTO Pregunta { get; set; }
		public PreguntaIntentoDTO PreguntaIntento { get; set; }
		public List<RespuestaPreguntaDTO> RespuestaPregunta { get; set; }
		public string Usuario { get; set; }
	}
	public class PreguntaProgramaCapacitacionRegistradaDTO
	{
		public int Id { get; set; }
		public string Enunciado { get; set; }
		public int IdTipoRespuesta { get; set; }
		public int? IdPreguntaTipo { get; set; }
		public int? MinutosPorPregunta { get; set; }
		public bool? RespuestaAleatoria { get; set; }
		public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
		public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
		public bool? MostrarFeedbackInmediato { get; set; }
		public bool? MostrarFeedbackPorPregunta { get; set; }
		public int? NumeroMaximoIntento { get; set; }
		public bool? ActivarFeedbackMaximoIntento { get; set; }
		public string MensajeFeedbackIntento { get; set; }
		public int? IdPGeneral { get; set; }
		public int? IdPEspecifico { get; set; }
		public string PGeneral { get; set; }
		public int? IdCapitulo { get; set; }
		public int? IdSesion { get; set; }
		public int? IdTipoRespuestaCalificacion { get; set; }
		public int? FactorRespuesta { get; set; }
		public string GrupoPregunta { get; set; }
		public int? IdTipoMarcador { get; set; }
		public decimal? ValorMarcador { get; set; }
		public int? OrdenPreguntaGrupo { get; set; }
		public int? IdPreguntaIntento { get; set; }
	}

	public class PreguntaProgramaCapacitacionAgrupadaDTO
	{
		public int Id { get; set; }
		public string Enunciado { get; set; }
		public int IdTipoRespuesta { get; set; }
		public int? IdPreguntaTipo { get; set; }
		public int? MinutosPorPregunta { get; set; }
		public bool? RespuestaAleatoria { get; set; }
		public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
		public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
		public bool? MostrarFeedbackInmediato { get; set; }
		public bool? MostrarFeedbackPorPregunta { get; set; }
		public int? NumeroMaximoIntento { get; set; }
		public bool? ActivarFeedbackMaximoIntento { get; set; }
		public string MensajeFeedbackIntento { get; set; }
		public List<int?> ListaPGeneral { get; set; }
		public List<string> PGeneral { get; set; }
		public int? IdTipoRespuestaCalificacion { get; set; }
		public int? FactorRespuesta { get; set; }
		public int? IdPreguntaCategoria { get; set; }
	}

    public class CompuestoPreguntasSecuenciaVideoDTO
    {
        public List<listadoPreguntaSecuenciaEstructuraDTO> Preguntas { get; set; }
        public string Usuario { get; set; }
    }

    public class listadoPreguntaSecuenciaEstructuraDTO
    {
        public int IdPgeneral { get; set; }
		public string GrupoPregunta { get; set; }
		public int IdTipoVista { get; set; }
        public int Segundos { get; set; }
        //public int OrdenPreguntaGrupo { get; set; }
        
        //public string EnunciadoPregunta { get; set; }
    }
}
