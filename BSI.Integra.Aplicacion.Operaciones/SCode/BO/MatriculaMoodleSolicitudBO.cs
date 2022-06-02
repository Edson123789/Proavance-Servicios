using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	///BO: MatriculaMoodleSolicitudBO
	///Autor: Jose Villena.
	///Fecha: 03/05/2021
	///<summary>
	///Columnas y funciones de la tabla T_MatriculaMoodleSolicitud
	///</summary>
	public class MatriculaMoodleSolicitudBO : BaseBO
	{
		///Propiedades							Significado
		///-------------						-----------------------
		///IdOportunidad						Fk de T_Oportunidad
		///IdCursoMoodle						Fk de T_CursoMoodle
		///IdUsuarioMoodle						Fk de T_UsuarioMoodle
		///CodigoMatricula						COdigo de Matricula
		///FechaInicioMatricula					Fecha Inicio Matricula
		///FechaFinMatricula					Fecha Fin Matricula
		///IdMatriculaMoodleSolicitudEstado		Fk de T_MatriculaMoodleSolicitudEstado
		///UsuarioSolicitud						Usuario Solicitud
		///FechaSolicitud						Fecha Solicitud
		///UsuarioAprobacion					Usuario Aprobacion
		///FechaAprobacion						Fecha de Aprobacion
		///IdMigracion							Id de migracion
		public int IdOportunidad { get; set; }
		public int IdCursoMoodle { get; set; }
		public int IdUsuarioMoodle { get; set; }
		public string CodigoMatricula { get; set; }
		public DateTime FechaInicioMatricula { get; set; }
		public DateTime FechaFinMatricula { get; set; }
		public int IdMatriculaMoodleSolicitudEstado { get; set; }
		public string UsuarioSolicitud { get; set; }
		public DateTime FechaSolicitud { get; set; }
		public string UsuarioAprobacion { get; set; }
		public DateTime? FechaAprobacion { get; set; }
		public int? IdMigracion { get; set; }
	}
}
