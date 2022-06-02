using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CursoComplementarioDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdTipoCursoComplementario { get; set; }
		public string TipoCursoComplementario { get; set; }


	}
	public class CursoComplementarioFiltroDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdTipoCursoComplementario { get; set; }
		public string Usuario { get; set; }
	}
	
}
