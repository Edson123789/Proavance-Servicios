using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TestimonioAlumnoMoodleDTO
    {
        public long UsuarioId { get; set; }
        public long CursoId { get; set; }
        public string Respuesta { get; set; }
        public string Completo { get; set; }
        public string Pregunta { get; set; }
    }
}
