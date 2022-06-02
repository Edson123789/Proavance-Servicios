using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/CompetenciaTecnica
	/// Autor: Luis Huallpa - Edgar Serruto
	/// Fecha: 07/09/2021
	/// <summary>
	/// BO para T_CompetenciaTecnica
	/// </summary>
	public class CompetenciaTecnicaBO : BaseBO
	{
		/// Propiedades						Significado
		/// -----------						------------
		/// IdTipoCompetenciaTecnica        Id de Tipo de Competencia Técnica
		/// Nombre							Nombre de Competencia Técnica
		/// IdMigracion						Id de Migración
		public int IdTipoCompetenciaTecnica { get; set; }
		public string Nombre { get; set; }
		public int? IdMigracion { get; set; }
	}
}
