using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PersonalDireccionVistaDTO
	{
		public int? IdPais { get; set; }
		public int? IdCiudad { get; set; }
		public string Distrito { get; set; }
		public string TipoVia { get; set; }
		public string NombreVia { get; set; }
		public string Manzana { get; set; }
		public int? Lote { get; set; }
		public string TipoZonaUrbana { get; set; }
		public string NombreZonaUrbana { get; set; }
		public bool Activo { get; set; }
		public string UsuarioModificacion { get; set; }
		public DateTime FechaModificacion { get; set; }
	}
}
