using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
	public class MatriculaCabeceraBeneficiosBO : BaseBO
	{
		public int IdMatriculaCabecera { get; set; }
		public string Nombre { get; set; }
		public int IdSuscripcionProgramaGeneral { get; set; }
		public Guid? IdMigracion { get; set; }
		public int? IdConfiguracionBeneficioProgramaGeneral { get; set; }
		public int? IdEstadoMatriculaCabeceraBeneficio { get; set; }
		public DateTime? FechaSolicitud { get; set; }
		public DateTime? FechaAprobacion { get; set; }
		public DateTime? FechaProgramada { get; set; }
		public DateTime? FechaEntrega { get; set; }

		public int? IdEstadoSolicitudBeneficio { get; set; }
		public int? Duracion { get; set; }
		public string UsuarioAprobacion { get; set; }
		public string UsuarioEntregoBeneficio { get; set; }
	}
}
