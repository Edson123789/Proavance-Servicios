using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RaListadoMinimoSesionDTO
    {
        public int IdRaSesion { get; set; }
        public DateTime? Fecha { get; set; }
        public string Horario { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFin { get; set; }
        public string NombreExpositor { get; set; }
        public string Tipo { get; set; }
        public string NombreSede { get; set; }
        public string NombreAula { get; set; }
		public bool BoletoAereo { get; set; }
    }
}
