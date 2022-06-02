using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampoFormularioSeleccionadoDTO
    {
        public int Id { get; set; }
		public int IdFormularioSolicitud { get; set; }
        public int IdCampoContacto { get; set; }
        public int NroVisitas { get; set; }
        public string Codigo { get; set; }
        public bool? Estado { get; set; }
        public string Nombre { get; set; }
        public bool? Siempre { get; set; }
        public bool? Inteligente { get; set; }
        public bool? Probabilidad { get; set; }
    }
}
