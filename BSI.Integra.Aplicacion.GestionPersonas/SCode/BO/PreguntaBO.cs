using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class PreguntaBO : BaseBO
	{
		public int? IdTipoRespuesta { get; set; }
		public int? IdPreguntaEscalaValor { get; set; }
		public string EnunciadoPregunta { get; set; }
		public bool? ConparacionValor { get; set; }
		public int? IdMigracion { get; set; }
		public bool? RequiereTiempo { get; set; }
		public int? MinutosPorPregunta { get; set; }
		public bool? RespuestaAleatoria { get; set; }
		public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
		public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
		public bool? MostrarFeedbackInmediato { get; set; }
		public bool? MostrarFeedbackPorPregunta { get; set; }
		public int? IdPreguntaIntento { get; set; }
		public int? IdPreguntaTipo { get; set; }
		public int? IdTipoRespuestaCalificacion { get; set; }
		public int? FactorRespuesta { get; set; }
		public int? IdPreguntaCategoria { get; set; }
	}
}
