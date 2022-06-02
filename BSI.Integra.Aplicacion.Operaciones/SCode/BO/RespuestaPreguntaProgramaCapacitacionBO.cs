using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	/// BO: Gestion de Personas/RespuestaPreguntaProgramaCapacitacion
    /// Autor: Luis Huallpa
    /// Fecha: 21/02/2021
    /// <summary>
    /// BO para la logica de la respuesta de pregunta de un programa capacitacion
    /// </summary>
	public class RespuestaPreguntaProgramaCapacitacionBO : BaseBO
	{
		/// Propiedades	                                Significado
        /// -----------	                                ------------
        /// IdPreguntaProgramaCapacitacion              Id de la pregunta de programa de capacitacion (PK de la tabla ope.T_PreguntaProgramaCapacitacion)
        /// RespuestaCorrecta							Flag para determinar si es la respuesta correcta
		/// NroOrden									Numero de orden de la pregunta
		/// EnunciadoRespuesta							Cadena con el enunciado de la pregunta
		/// IdMigracion									Id de migracion de V3 (nullable)
		/// NroOrdenRespuesta							Numero de orden de la respuesta
		/// Puntaje										Puntaje de la pregunta
		/// FeedbackPositivo							Cadena con el feedback positivo
		/// FeedbackNegativo							Cadena con el feedback negativo
        /// MostrarFeedBack								Flag para mostrar o no el feedback
		/// PuntajeTipoRespuesta						Puntaje segun el tipo de respuesta
		
		public int? IdPreguntaProgramaCapacitacion { get; set; }
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
