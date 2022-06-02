using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	/// BO: Gestion de Personas/PreguntaProgramaCapacitacion
    /// Autor: Luis Huallpa
    /// Fecha: 21/02/2021
    /// <summary>
    /// BO para la logica del detalle de las preguntas de una capacitacion
    /// </summary>
	public class PreguntaProgramaCapacitacionBO : BaseBO
	{
		/// Propiedades	                                Significado
        /// -----------	                                ------------
        /// IdPgeneral									Id del programa general (PK de la tabla pla.T_PGeneral)
        /// OrdenFilaCapitulo							Orden de la fila del capitulo
		/// OrdenFilaSesion								Orden de la fila del sesion
		/// IdTipoRespuesta								Id del tipo de respuesta
		/// IdPreguntaEscalaValor						Id de la pregunta escala valor (PK de la tabla gp.T_PreguntaEscalaValor)
		/// EnunciadoPregunta							Cadena con el enunciado de la pregunta
		/// IdMigracion									Id de migracion de V3 (nullable)
		/// RequiereTiempo								Flag para verificar si requiere tiempo
		/// MinutosPorPregunta							Entero con los minutos por pregunta
		/// RespuestaAleatoria							Flag para verificar si la respuesta aleatoria
		/// ActivarFeedBackRespuestaCorrecta			Flag para determinar si se usara un feedback para las respuestas correctas
		/// ActivarFeedBackRespuestaIncorrecta			Flag para determinar si se usara un feedback para las respuestas incorrectas
		/// MostrarFeedbackInmediato					Flag para determinar si se mostrara un feedback inmediato					
		/// MostrarFeedbackPorPregunta					Flag para determinar si se mostrara un feedback por preguntas
		/// IdPreguntaIntento							Id del intento de pregunta (PK de la tabla gp.T_PreguntaIntento)
		/// IdPreguntaTipo								Id del tipo de pregunta (PK de la tabla gp.T_PreguntaTipo)
		/// IdTipoRespuestaCalificacion					Id del tipo de respuesta calificacion (PK de la tabla gp.T_TipoRespuestaCalificacion)
		/// FactorRespuesta								Entero con el factor de respuesta
		/// GrupoPregunta								Cadena con el grupo de pregunta al cual esta enlazado
		/// IdTipoMarcador								Id con el tipo de marcado (PK de la tabla ope.T_TipoMarcador)
		/// ValorMarcador								Entero con el valor del marcador
		/// OrdenPreguntaGrupo							Entero con el orden del grupo de pregunta
        /// IdPespecifico								Id del programa especifico (PK de la tabla pla.T_PEspecifico)


		public int IdPgeneral { get; set; }
		public int? OrdenFilaCapitulo { get; set; }
		public int? OrdenFilaSesion { get; set; }
		public int? IdTipoRespuesta { get; set; }
		public int? IdPreguntaEscalaValor { get; set; }
		public string EnunciadoPregunta { get; set; }
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
		public string GrupoPregunta { get; set; }
		public int? IdTipoMarcador { get; set; }
		public decimal? ValorMarcador { get; set; }
		public int? OrdenPreguntaGrupo { get; set; }
		public int? IdPespecifico { get; set; }
	}
}
