using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class HotelDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Telefono { get; set; }
		public int IdCiudad { get; set; }
		public string Ciudad { get; set; }
		public string Direccion { get; set; }
		public bool CubrimosDesayuno { get; set; }
		public int? IdRaSede { get; set; }
	}
}
