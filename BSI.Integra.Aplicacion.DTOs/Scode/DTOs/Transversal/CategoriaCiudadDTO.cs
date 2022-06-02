using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CategoriaCiudadDTO
	{
		public int Id { get; set; }
		public int IdCategoriaPrograma { get; set; }
		public int? IdCiudad { get; set; }
		public string TroncalCompleto { get; set; }
		public int IdRegionCiudad { get; set; }
	}
}
