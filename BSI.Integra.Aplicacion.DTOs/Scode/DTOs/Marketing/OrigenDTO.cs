using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class OrigenDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Descripcion { get; set; }
		public int IdTipoDato { get; set; }
		public int Prioridad { get; set; }
		public int IdCategoriaOrigen { get; set; }
		public int Total { get; set; }
	}
}
