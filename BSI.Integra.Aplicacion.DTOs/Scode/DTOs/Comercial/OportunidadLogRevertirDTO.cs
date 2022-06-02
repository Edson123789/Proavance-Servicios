using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class OportunidadLogRevertirDTO
	{
		public int Id { get; set; }
		public int? IdClasificacionPersona { get; set; }
		public int? IdOportunidad { get; set; }
		public int? IdCentroCosto { get; set; }
		public int? IdPersonalAsignado { get; set; }
		public int? IdFaseOportunidad { get; set; }
		public int? IdTipoDato { get; set; }
		public int? IdContacto { get; set; }
		public DateTime? FechaLog { get; set; }
	}
}
