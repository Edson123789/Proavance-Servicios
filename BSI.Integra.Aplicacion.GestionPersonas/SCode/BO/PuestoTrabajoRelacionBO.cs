using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoRelacionBO
	///Autor: Luis H, Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajoRelacion
	///</summary>
	public class PuestoTrabajoRelacionBO : BaseBO
    {
		///Propiedades				Significado
		///-------------			-----------------------
		///IdPerfilPuestoTrabajo    FK de T_PerfilPuestoTrabajo
		///IdMigracion				Id de Migración
		public int IdPerfilPuestoTrabajo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
