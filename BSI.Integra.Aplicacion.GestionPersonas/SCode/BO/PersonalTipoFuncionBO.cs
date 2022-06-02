using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/PersonalTipoFuncion
	/// Autor: Luis Huallpa
	/// Fecha: 08/09/2021
	/// <summary>
	/// BO para T_PersonalTipoFuncion
	/// </summary>
	public class PersonalTipoFuncionBO : BaseBO
	{
		/// Propiedades		    Significado
		/// -----------		    ------------
		/// Nombre			    Nombre de Tipo de Función de Personal
		/// IdMigracion         Id de Migración
		public string Nombre { get; set; }
		public int? IdMigracion { get; set; }
	}
}
