using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CodigoFurDTO
	{
		public int Id { get; set; }
		public string Codigo { get; set; }

	}
    public class CodigoFurProveedorDTO
	{
		public int Id { get; set; }
		public int IdProveedor { get; set; }
		public string RazonSocial { get; set; }
		public string Codigo { get; set; }

	}
}
