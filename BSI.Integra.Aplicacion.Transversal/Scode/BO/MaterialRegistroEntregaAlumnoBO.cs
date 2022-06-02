using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
	public class MaterialRegistroEntregaAlumnoBO : BaseBO
	{
		public int IdMatriculaCabecera { get; set; }
		public int IdMaterialPespecificoDetalle { get; set; }
		public int IdEstadoEntregaMaterialAlumno { get; set; }
		public int? IdMigracion { get; set; }
		public string UsuarioAprobacion { get; set; }
		public DateTime? FechaAprobacion { get; set; }
	}
}
