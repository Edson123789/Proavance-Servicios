using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class OportunidadOperacionesFiltroDTO
	{
		public int Id { get; set; }
		public int IdPersonal_Asignado { get; set; }
		public int IdAlumno { get; set; }
		public int IdCentroCosto { get; set; }
		public int? IdPadre { get; set; }
		public int IdFaseOportunidad { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
		public int? IdOportunidadClasificacionOperaciones { get; set; }
	}
}
