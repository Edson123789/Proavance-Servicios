using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Finanzas.BO
{
	public class OportunidadIsVerificadaBO : BaseBO
	{
		public int IdOportunidad { get; set; }
		public int IdMatriculaCabecera { get; set; }
		public bool Verificado { get; set; }
		public Guid? IdMigracion { get; set; }
	}
}
