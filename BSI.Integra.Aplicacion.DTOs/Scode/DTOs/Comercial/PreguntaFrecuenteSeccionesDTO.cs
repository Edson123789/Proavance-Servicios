using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreguntaFrecuenteSeccionesDTO
    {
        public int IdPrograma { get; set; }
        public int IdSeccion { get; set; }
        public string Nombre { get; set; }
        public List<PreguntaFrecuenteRespuestasDTO> Contenido { get; set; }
    }
}
