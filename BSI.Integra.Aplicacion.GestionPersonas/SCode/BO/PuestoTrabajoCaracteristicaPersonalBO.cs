using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoCaracteristicaPersonalBO
	///Autor: Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajoCaracteristicaPersonal
	///</summary>
	public class PuestoTrabajoCaracteristicaPersonalBO : BaseBO
    {
		///Propiedades				Significado
		///-------------			-----------------------
		///IdPerfilPuestoTrabajo    FK de T_PerfilPuestoTrabajo
		///EdadMinima				Edad Mínima Personal
		///EdadMaxima				Edad Máxima Personal
		///IdSexo					FK de T_Sexo
		///IdEstadoCivil			FK de T_EstadoCivil
		///IdMigracion				Ide de Migración
		public int IdPerfilPuestoTrabajo { get; set; }
		public int? EdadMinima { get; set; }
		public int? EdadMaxima { get; set; }
		public int IdSexo { get; set; }
		public int IdEstadoCivil { get; set; }
		public int? IdMigracion { get; set; }
	}
}
