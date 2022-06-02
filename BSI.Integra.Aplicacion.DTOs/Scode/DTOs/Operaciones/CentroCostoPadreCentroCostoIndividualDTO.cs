using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class CentroCostoPadreCentroCostoIndividualDTO
	{
		public int IdCentroCosto { get; set; }
		public int IdProgramaEspecifico { get; set; }
		public string CentroCosto { get; set; }
		public string ProgramaEspecifico { get; set; }
		public string EstadoProgramaEspecifico { get; set; }
		public string Tipo { get; set; }
	}
}
