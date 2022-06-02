using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoRelacionDetalleBO
	///Autor: Luis H, Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajoRelacionDetalle
	///</summary>
	public class PuestoTrabajoRelacionDetalleBO : BaseBO
	{
		///Propiedades						Significado
		///-------------					-----------------------
		///IdPuestoTrabajoRelacion			FK de T_PerfilPuestoTrabajo
		///IdPuestoTrabajoDependencia		FK de T_PuestoTrabajoDependencia
		///IdPuestoTrabajoPuestoAcargo		FK de T_PuestoTrabajoPuestoAcargo
		///IdPersonalAreaTrabajo			FK de T_PersonalAreaTrabajo
		///IdMigracion						Id de Migración
		public int IdPuestoTrabajoRelacion { get; set; }
		public int? IdPuestoTrabajoDependencia { get; set; }
		public int? IdPuestoTrabajoPuestoAcargo { get; set; }
		public int? IdPersonalAreaTrabajo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
