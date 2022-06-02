using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ProveedorProductoDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Simbolo { get; set; }
		public string NombreMoneda { get; set; }
		public decimal Precio { get; set; }
		public int IdProducto { get; set; }
		public string Presentacion { get; set; }
		public int IdHistorico { get; set; }
		public int Version { get; set; }
	}
}
