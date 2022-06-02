using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/NivelCompetenciaTecnica
	/// Autor: Luis Huallpa - Edgar Serruto
	/// Fecha: 07/09/2021
	/// <summary>
	/// BO para T_NivelCompetenciaTecnicaBO
	/// </summary>
	public class NivelCompetenciaTecnicaBO : BaseBO
	{
		/// Propiedades	       Significado
		/// -----------	       ------------
		/// Nombre			   Nombre de Nivel de Competencia Técnica
		/// IdMigracion        Id de Migración
		public string Nombre { get; set; }
		public int? IdMigracion { get; set; }
	}
}
