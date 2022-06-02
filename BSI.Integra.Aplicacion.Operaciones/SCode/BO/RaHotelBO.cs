using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
	public class RaHotelBO : BaseBO
	{
		public string Nombre { get; set; }
		public string Telefono { get; set; }
		public int IdCiudad { get; set; }
		public string Direccion { get; set; }
		public bool CubrimosDesayuno { get; set; }
		public int? IdRaSede { get; set; }
		public int? IdMigracion { get; set; }
	}
}
