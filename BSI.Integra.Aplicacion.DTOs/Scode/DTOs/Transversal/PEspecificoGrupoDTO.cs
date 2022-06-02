using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class PEspecificoGrupoDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdPEspecificoPadre { get; set; }
	}
	public class PEspecificoGrupoComboDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdPEspecificoPadre { get; set; }
		public List<PEspecificoGrupoAgrupadoDTO> Grupos { get; set; }
	}
	public class PEspecificoGrupoAgrupadoDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
	}
}
