using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TestimonioProgramaMoodleDTO
    {
        public long UsuarioId { get; set; }
        public long CursoId { get; set; }
        public string Titulo { get; set; }
        public string Alumno { get; set; }
        public string Autoriza { get; set; }
    }
}
