using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.GestionPersonas.BO
{
	public class ProcesoSeleccionBO :BaseBO
	{
		public string Nombre { get; set; }
		public int IdPuestoTrabajo { get; set; }
		public string Codigo { get; set; }
		public string Url { get; set; }
		public bool? Activo { get; set; }
		public int? IdSede { get; set; }
		public int? IdMigracion { get; set; }
        public DateTime? FechaInicioProceso { get; set; }
        public DateTime? FechaFinProceso { get; set; }
    }
}
