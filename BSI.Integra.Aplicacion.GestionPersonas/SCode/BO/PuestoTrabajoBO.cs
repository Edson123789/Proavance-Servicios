using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoBO
	///Autor: Luis H, Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajo
	///</summary>
	public class PuestoTrabajoBO : BaseBO
	{
		///Propiedades                  Significado
		///-------------                -----------------------
		///Nombres                      Nombres de Puesto de Trabajo
		///IdMigracion                  Id Migracion
		///IdPersonalAreaTrabajo        FK de T_PersonalAreaTrabajo
		
		public string Nombre { get; set; }
		public int? IdMigracion { get; set; }
		public int? IdPersonalAreaTrabajo { get; set; }
	}
}
