using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	///BO: PuestoTrabajoExperienciaBO
	///Autor: Luis H, Edgar S.
	///Fecha: 27/01/2021
	///<summary>
	///Columnas de la tabla T_PuestoTrabajoExperiencia
	///</summary>
	public class PuestoTrabajoExperienciaBO : BaseBO
	{
		///Propiedades				Significado
		///-------------			-----------------------
		///IdPerfilPuestoTrabajo	FK de T_PerfilPuestoTrabajo
		///IdExperiencia			FK de T_Experiencia
		///IdTipoExperiencia		FK de T_TipoExperiencia
		///NumeroMinimo				Número Mínimo
		///Periodo					Periodo
		///IdMigracion				Ide Migración
		public int IdPerfilPuestoTrabajo { get; set; }
		public int IdExperiencia { get; set; }
		public int IdTipoExperiencia { get; set; }
		public int NumeroMinimo { get; set; }
		public string Periodo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
