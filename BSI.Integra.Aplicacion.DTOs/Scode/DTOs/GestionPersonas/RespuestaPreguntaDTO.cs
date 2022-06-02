using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class RespuestaPreguntaDTO
	{
		public int Id { get; set; }
		public int IdPregunta { get; set; }
		public bool RespuestaCorrecta { get; set; }
		public int? NroOrdenRespuesta { get; set; }
		public int NroOrden { get; set; }
		public string EnunciadoRespuesta { get; set; }
		public int? Puntaje { get; set; }
		public string FeedbackPositivo { get; set; }
		public string FeedbackNegativo { get; set; }
	}

	public class RespuestaPreguntaProgramaCapacitacionDTO
	{
		public int Id { get; set; }
		public int IdPreguntaProgramaCapacitacion { get; set; }
		public bool RespuestaCorrecta { get; set; }
		public int? NroOrdenRespuesta { get; set; }
		public int NroOrden { get; set; }
		public string EnunciadoRespuesta { get; set; }
		public int? Puntaje { get; set; }
		public string FeedbackPositivo { get; set; }
		public string FeedbackNegativo { get; set; }
	}
}
