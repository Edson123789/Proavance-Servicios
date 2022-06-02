using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class RevertirFaseOportunidadFiltroDTO
	{
		public int IdOportunidad { get; set; }
		public DateTime? FechaProgramada { get; set; }
		public string Usuario { get; set; }
	}
}
