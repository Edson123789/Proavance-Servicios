using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/TipoExperiencia
	/// Autor: Luis Huallpa
	/// Fecha: 25/09/2020
	/// <summary>
	/// BO para la logica de T_TipoExperiencia
	/// </summary>
	public class TipoExperienciaBO : BaseBO
	{
		/// Propiedades             Significado
		/// -----------	            ------------
		/// Codigo                  Nombre de Tipo de Experiencia
		/// Id Migración			Id de Migración
		public string Nombre { get; set; }
		public int? IdMigracion { get; set; }
	}
}
