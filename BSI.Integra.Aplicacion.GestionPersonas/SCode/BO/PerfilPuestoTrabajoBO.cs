using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PerfilPuestoTrabajoBO
	///Autor: Luis H, Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PerfilPuestoTrabajo
	///</summary>
	public class PerfilPuestoTrabajoBO : BaseBO
	{
		///Propiedades							Significado
		///-------------						-----------------------
		///IdPuestoTrabajo						FK de T_PuestoTrabajo
		///Descripcion							Descripción de Perfil de Puesto de Trabajo
		///Objetivo								Objetivo de Puesto de Trabajo
		///Version								Versión de Puesto de Trabajo
		///EsActual								Estado de versión actual
		///IdMigracion							Id de migración
		///IdPersonalSolicitud					FK de T_Personal que hizo solicitud de modificación
		///FechaSolicitud						Fecha de Solicitud
		///IdPersonalAprobacion					Fk de T_Personal aprobación de cambio
		///FechaAprobacion						Fecha de Aprobación
		///Observacion							Observaciones realizadas
		///IdPerfilPuestoTrabajoEstadoSolicitud FK de PerfilPuestoTrabajoEstadoSolicitud
		public int IdPuestoTrabajo { get; set; }
		public string Descripcion { get; set; }
		public string Objetivo { get; set; }
		public int Version { get; set; }
		public bool? EsActual { get; set; }
		public int? IdMigracion { get; set; }
		public int? IdPersonalSolicitud { get; set; }
		public DateTime? FechaSolicitud { get; set; }
		public int? IdPersonalAprobacion { get; set; }
		public DateTime? FechaAprobacion { get; set; }
		public string Observacion { get; set; }
		public int? IdPerfilPuestoTrabajoEstadoSolicitud { get; set; }
	}
}
