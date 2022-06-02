using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampoFormularioDTO
    {
        public int? IdFormularioSolicitud { get; set; }
        public int? IdCampoContacto { get; set; }
        public int? NroVisitas { get; set; }
        public string Codigo { get; set; }
        public string Campo { get; set; }
        public bool? Siempre { get; set; }
        public bool? Inteligente { get; set; }
        public bool? Probabilidad { get; set; }
    }
}
