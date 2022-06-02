using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class NivelEstudioDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdTipoFormacion { get; set; }
		public string TipoFormacion { get; set; }
	}
	
	public class NivelEstudioFiltroDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdTipoFormacion { get; set; }
		public string Usuario { get; set; }
	}
}
