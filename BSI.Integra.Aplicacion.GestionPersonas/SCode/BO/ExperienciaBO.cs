using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/Experiencia
	/// Autor: Britsel Calluchi - Luis Huallpa
	/// Fecha: 08/09/2021
	/// <summary>
	/// BO para T_Experiencia
	/// </summary>
	public class ExperienciaBO : BaseBO
	{
		/// Propiedades		    Significado
		/// -----------		    ------------
		/// Nombre			    Nombre de Tipo de Experiencia
		/// IdAreaTrabajo		Id de Área de Trabajo
		/// IdMigración			Id de Migración
		public string Nombre { get; set; }
		public int IdAreaTrabajo { get; set; }
		public int? IdMigracion { get; set; }
	}
}
