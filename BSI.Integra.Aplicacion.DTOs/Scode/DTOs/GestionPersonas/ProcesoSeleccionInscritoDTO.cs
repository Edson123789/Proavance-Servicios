using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ProcesoSeleccionInscritoDTO
	{
		public int Id { get; set; }
		public int IdPostulante { get; set; }
		public string Postulante { get; set; }
		public int IdProcesoSeleccion { get; set; }
		public string ProcesoSeleccion { get; set; }
		public int IdPuestoTrabajo { get; set; }
		public string PuestoTrabajo { get; set; }
		public int? IdSede { get; set; }
		public string Sede { get; set; }
		public DateTime FechaRegistro { get; set; }

	}
}
