using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class GrupoFiltroSesionDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
	}

	public class TipoProgramaPEspecificoDTO
	{
		public int IdPGeneral { get; set; }
		public int IdPEspecifico { get; set; }
		public int IdTipoPrograma { get; set; }
	}
}
