using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: ExamenRealizadoRespuestaEvaluadorBO
	///Autor: Britsel C., Luis H.,Edgar S.
	///Fecha: 03/03/2021
	///<summary>
	///Columnas y funciones de la tabla T_ExamenRealizadoRespuestaEvaluador
	///</summary>
	public class ExamenRealizadoRespuestaEvaluadorBO : BaseBO
	{
		///Propiedades		            Significado
		///-------------	            --------------
		///IdExamenAsignadoEvaluador    Id de Examen Asignado a evaluador
		///IdPregunta                   Id de Pregunta
		///IdRespuestaPregunta          Id de Respueta Pregunta
		///TextoRespuesta               Texto de Respuesta
		///IdMigracion                  Id Migración
		public int IdExamenAsignadoEvaluador { get; set; }
		public int IdPregunta { get; set; }
		public int IdRespuestaPregunta { get; set; }
		public string TextoRespuesta { get; set; }
		public int? IdMigracion { get; set; }
	}
}
