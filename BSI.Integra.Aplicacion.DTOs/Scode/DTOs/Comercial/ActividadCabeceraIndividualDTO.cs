using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ActividadCabeceraIndividualDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdActividadBase { get; set; }
        public int IdPlantilla { get; set; }
		public int IdPersonalAreaTrabajo { get; set; }
        public bool? EsEnvioMasivo { get; set; }

    }
}
