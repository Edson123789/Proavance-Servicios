using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: Gestion de Personas/PreguntaIntentoDetalle
    /// Autor: Luis Huallpa
    /// Fecha: 21/02/2021
    /// <summary>
    /// BO para la logica del detalle de las preguntas intentos de capacitacion
    /// </summary>
	public class PreguntaIntentoDetalleBO : BaseBO
	{
		/// Propiedades	                                Significado
        /// -----------	                                ------------
        /// IdPreguntaIntento                           Id de la pregunta intento (PK de la tabla gp.T_PreguntaIntento)
        /// PorcentajeCalificacion                      Porcentaje de calificacion
        /// IdMigracion                                 Id de migracion de V3 (nullable)
        
		public int IdPreguntaIntento { get; set; }
		public int? PorcentajeCalificacion { get; set; }
		public int? IdMigracion { get; set; }
	}
}
