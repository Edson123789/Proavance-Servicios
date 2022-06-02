using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SolicitudCambioCronograma
    {
        public string CodigoMatricula { get; set; }
        public int AprobadoPorId { get; set; }
        public int SolicitadoPorId { get; set; }
        public string Comentario { get; set; }
    }
}
