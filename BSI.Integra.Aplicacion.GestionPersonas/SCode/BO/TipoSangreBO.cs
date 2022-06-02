using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	/// BO: GestionPersonas/TipoSangre
	/// Autor: Edgar Serruto
	/// Fecha: 07/09/2021
	/// <summary>
	/// BO para T_TipoSangre
	/// </summary>
	public class TipoSangreBO : BaseBO
	{
		/// Propiedades	       Significado
		/// -----------	       ------------
		/// TipoSangre         Nombre de Tipo de Sangre
		/// Comentario         Comentario
		/// IdMigracion        Id de Migración
		public string TipoSangre { get; set; }
		public string Comentario { get; set; }
		public int? IdMigracion { get; set; }
	}
}
