using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class FurSesionFiltroDTO
	{
		public int Id { get; set; }
		public int IdProducto { get; set; }
		public int IdProveedor { get; set; }
		public string Factor { get; set; }
		public decimal Cantidad { get; set; }
		public string Usuario { get; set; }
		public int AreaTrabajo { get; set; }
		public int Semana { get; set; }
		public int Ciudad { get; set; }
	}

	public class FurRegistroMaterialDTO
	{
		public int Id { get; set; }
		public int IdMaterialPEspecificoDetalle { get; set; }
		public int IdProducto { get; set; }
		public int IdProveedor { get; set; }
		public decimal Cantidad { get; set; }
		public string Usuario { get; set; }
		public DateTime FechaEntrega { get; set; }
		public string DireccionEntrega { get; set; }
	}
}
