using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ProgramaEspecificoFURDTO
	{
		public int Id { get; set; }
		public string Codigo { get; set; }
		public string Proveedor { get; set; }
		public string Producto { get; set; }
		public string CentroCosto { get; set; }
		public string Unidades { get; set; }
		public string Descripcion { get; set; }
		public string Ciudad { get; set; }
		public int IdProveedor { get; set; }
		public int IdProducto { get; set; }
		public int IdCentroCosto { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
		public int IdCiudad { get; set; }
	}
}
