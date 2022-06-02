using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class MaterialPEspecificoDetalleFurDTO
	{
		public int IdMaterialPEspecificoDetalle { get; set; }
		public int IdFur { get; set; }
		public int IdProveedor { get; set; }
		public int IdProducto { get; set; }
		public string Monto { get; set; }
		public double Cantidad { get; set; }
		public string NombrePlural { get; set; }
		public string Simbolo { get; set; }
		public DateTime? FechaEntrega { get; set; }
		public string DireccionEntrega { get; set; }
	}
}
