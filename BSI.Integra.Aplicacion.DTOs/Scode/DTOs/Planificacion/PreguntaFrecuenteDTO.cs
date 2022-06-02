using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PreguntaFrecuenteDTO
    {
        public int Id { get; set; }
        public int IdSeccionPreguntaFrecuente { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int Tipo { get; set; }
    }
}
