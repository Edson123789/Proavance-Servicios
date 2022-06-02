using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones
{
	public class CapituloSesionProgramaCapacitacionDTO
	{
		public int IdPGeneral { get; set; }
		public int IdCapituloProgramaCapacitacion { get; set; }
		public string CapituloProgramaCapacitacion { get; set; }
		public List<SesionSubSeccionProgramaCapacitacionDTO> ListaSesionesProgramaCapacitacion { get; set; }
	}

	public class SesionSubSeccionProgramaCapacitacionDTO
	{
		public int IdSesionProgramaCapacitacion { get; set; }
		public string SesionProgramaCapacitacion { get; set; }
		public List<SubSeccionProgramaCapacitacionDTO> ListaSubSeccionProgramaCapacitacion { get; set; }
	}

	public class SubSeccionProgramaCapacitacionDTO
    {
		public int IdSesionProgramaCapacitacion { get; set; }
		public string SubSeccionProgramaCapacitacion { get; set; }
    }
}
