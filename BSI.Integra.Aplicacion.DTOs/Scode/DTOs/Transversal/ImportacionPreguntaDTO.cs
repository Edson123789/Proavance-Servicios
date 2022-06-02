using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ImportacionPreguntaDTO
	{

	}
	public class ImportacionPreguntaRespuestaProgramaCapacitacionDTO
	{
		public int? NumeroMaximoIntento { get; set; }
		public bool? ActivarFeedbackMaximoIntento { get; set; }
		public string MensajeFeedback { get; set; }
		public int? IdTipoRespuesta { get; set; } 
		public string EnunciadoPregunta { get; set; } 
		public int? MinutosPorPregunta { get; set; } 
		public bool? RespuestaAleatoria { get; set; } 
		public bool? ActivarFeedBackRespuestaCorrecta { get; set; } 
		public bool? ActivarFeedBackRespuestaIncorrecta { get; set; } 
		public bool? MostrarFeedbackInmediato { get; set; } 
		public bool? MostrarFeedbackPorPregunta { get; set; } 
		public int? IdPreguntaTipo { get; set; } 
		public int? IdTipoRespuestaCalificacion { get; set; } 
		public int? FactorRespuesta { get; set; } 
		public int IdPgeneral { get; set; }
		public int? IdPEspecifico { get; set; }
		public int? OrdenFilaCapitulo { get; set; } 
		public string Sesion { get; set; }
		public string SubSeccion { get; set; }
		public string GrupoPregunta { get; set; }
		public int? IdTipoMarcador { get; set; }
		public decimal? ValorMarcador { get; set; }
		public int? OrdenPreguntaGrupo { get; set; }
		//============RESPUESTAS=============================

		public bool? RespuestaCorrecta { get; set; }
		public int NroOrden { get; set; }
		public string EnunciadoRespuesta { get; set; }
		public int? NroOrdenRespuesta { get; set; }
		public int? Puntaje { get; set; }
		public string FeedbackPositivo { get; set; }
		public string FeedbackNegativo { get; set; }

		//====================PREGUNTAINTENTO=============
		public int? PorcentajeCalificacion { get; set; }
	}

	public class PreguntaIntentoAgrupadoDTO
	{
		public int? NumeroMaximoIntento { get; set; }
		public bool? ActivarFeedbackMaximoIntento { get; set; }
		public string MensajeFeedback { get; set; }
		public List<PreguntaIntentoDetalleAgrupadoDTO> PreguntaIntentoDetalle { get; set; }
	}
	public class PreguntaIntentoDetalleAgrupadoDTO
	{
		public int? PorcentajeCalificacion { get; set; }
	}
	public class PreguntaProgramaCapacitacionAgrupadoDTO
	{
		public int? IdTipoRespuesta { get; set; }
		public string EnunciadoPregunta { get; set; }
		public int? MinutosPorPregunta { get; set; }
		public bool? RespuestaAleatoria { get; set; }
		public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
		public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
		public bool? MostrarFeedbackInmediato { get; set; }
		public bool? MostrarFeedbackPorPregunta { get; set; }
		public int? IdPreguntaTipo { get; set; }
		public int? IdTipoRespuestaCalificacion { get; set; }
		public int? FactorRespuesta { get; set; }
		public int IdPgeneral { get; set; }
		public int? IdPEspecifico { get; set; }
		public int? OrdenFilaCapitulo { get; set; }
		public string Sesion { get; set; }
		public string SubSeccion { get; set; }
		public string GrupoPregunta { get; set; }
		public int? IdTipoMarcador { get; set; }
		public decimal? ValorMarcador { get; set; }
		public int? OrdenPreguntaGrupo { get; set; }
		public PreguntaIntentoAgrupadoDTO PreguntaIntento { get; set; }
		public List<RespuestaPreguntaProgramaCapacitacionAgrupadoDTO> RespuestaPreguntaProgramaCapacitacion { get; set; }
	}

	public class RespuestaPreguntaProgramaCapacitacionAgrupadoDTO
	{
		public bool? RespuestaCorrecta { get; set; }
		public int NroOrden { get; set; }
		public string EnunciadoRespuesta { get; set; }
		public int? NroOrdenRespuesta { get; set; }
		public int? Puntaje { get; set; }
		public string FeedbackPositivo { get; set; }
		public string FeedbackNegativo { get; set; }
	}

	public class PreguntaProgramaCapacitacionExcelCompuestoDTO
	{
		public PreguntaProgramaCapacitacionAgrupadoDTO PreguntaProgramaCapacitacion { get; set; }
	}
}
