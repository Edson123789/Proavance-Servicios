using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
	public class ProcesoSeleccionDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdPuestoTrabajo { get; set; }
		public string PuestoTrabajo { get; set; }
		public string Codigo { get; set; }
		public string Url { get; set; }
		public string Activo { get; set; }
		public int? IdSede { get; set; }
		public string Sede { get; set; }

	}


    public class PostulanteUltimoProcesoSeleccionDTO
    {
        public int IdPostulante { get; set; }
        public string Postulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public string ProcesoSeleccion { get; set; }

    }
}
