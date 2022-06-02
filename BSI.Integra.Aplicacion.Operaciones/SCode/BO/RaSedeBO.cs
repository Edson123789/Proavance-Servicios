using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones
{
	public class RaSedeBO : BaseBO
	{
		public string Nombre { get; set; }
		public string Direccion { get; set; }
		public string Ubicacion { get; set; }
		public string Ciudad { get; set; }
		public int? IdRaRazonSocial { get; set; }
		public bool AplicaDocumentacion { get; set; }
		public int? IdMigracion { get; set; }
	}
}
