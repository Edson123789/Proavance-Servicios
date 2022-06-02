using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class AmbienteCiudadFiltroDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdLocacion { get; set; }
		public int IdCiudad { get; set; }
	}
}
