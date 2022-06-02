using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/PersonalRelacionExterna
	/// Autor: Luis Huallpa . 
	/// Fecha: 10/09/2020
	/// <summary>
	/// BO para la logica de T_PersonalRelacionExterna
	/// </summary>
	public class PersonalRelacionExternaBO : BaseBO
	{
		/// Propiedades             Significado
		/// -----------	            ------------
		/// Nombre                  Nombre de Relación Externa
		/// IdPersonalAreaTrabajo	Id de Area de Trabajo de Personal
		/// IdMigracion				Id de Migración
		public string Nombre { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
