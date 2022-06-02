using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CategoriaMoodleCursoDTO
	{
		public int Id { get; set; }
		public int IdCategoria { get; set; }
		public string NombreCategoria { get; set; }
		public string TipoCategoria { get; set; }
		public bool AplicaProyecto { get; set; }
	}
}
