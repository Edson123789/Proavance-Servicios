using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class RespuestaPreguntaBO : BaseBO
	{
		public int? IdPregunta { get; set; }
		public bool? RespuestaCorrecta { get; set; }
		public int NroOrden { get; set; }
		public string EnunciadoRespuesta { get; set; }
		public int? IdMigracion { get; set; }
		public int? NroOrdenRespuesta { get; set; }
		public int? Puntaje { get; set; }
		public string FeedbackPositivo { get; set; }
		public string FeedbackNegativo { get; set; }
		public bool? MostrarFeedBack { get; set; }
		public int? PuntajeTipoRespuesta { get; set; }
	}
}
