using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
	public class MaterialCriterioVerificacionDetalleBO : BaseBO
	{
		public int IdMaterialPespecificoDetalle { get; set; }
		public int IdMaterialCriterioVerificacion { get; set; }
		public bool EsAprobado { get; set; }
		public int? IdMigracion { get; set; }
	}
}
