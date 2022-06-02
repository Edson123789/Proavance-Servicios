using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class PostulanteConexionInternetBO : BaseBO
	{
		public int IdPostulante { get; set; }
		public string TipoConexion { get; set; }
		public string MedioConexion { get; set; }
		public string VelocidadInternet { get; set; }
		public string ProveedorInternet { get; set; }
		public decimal CostoInternet { get; set; }
		public string ConexionCompartida { get; set; }
		public int? IdMigracion { get; set; }
	}
}
