using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/PerfilPuestoTrabajoEstadoSolicitud
	/// Autor: Fischer Valdez - Wilber Choque - Joao - Priscila Pacsi - Luis Huallpa - Carlos Crispin - Esthephany Tanco - Gian Miranda
	/// Fecha: 09/07/2021
	/// <summary>
	/// BO para T_PerfilPuestoTrabajoEstadoSolicitud
	/// </summary>
	public class PerfilPuestoTrabajoEstadoSolicitudBO : BaseBO
	{
		/// Propiedades	       Significado
		/// -----------	       ------------
		/// Nombre             Nombre de Estado de Solicitud
		/// IdMigracion        Id de Migración
		public string Nombre { get; set; }
		public int? IdMigracion { get; set; }
	}
}
