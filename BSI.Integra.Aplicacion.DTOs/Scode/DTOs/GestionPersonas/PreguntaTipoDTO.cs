using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreguntaTipoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoRespuesta { get; set; }
        public int IdTipoRespuesta { get; set; }
        public string Usuario { get; set; }
    }
}
