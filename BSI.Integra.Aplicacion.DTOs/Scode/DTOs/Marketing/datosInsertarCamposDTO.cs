using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class datosInsertarCamposDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int? NroVisitas { get; set; }
        public bool? Siempre { get; set; }
        public bool? Inteligente { get; set; }
        public bool? Probabilidad { get; set; }
    }
}
