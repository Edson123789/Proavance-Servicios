using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TestimonioProgramaDTO
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public int CursoMoodleId { get; set; }
        public int UsuarioMoodleId { get; set; }
        public string Alumno { get; set; }
        public string Testimonio { get; set; }
        public string Pregunta { get; set; }
        public bool Autoriza { get; set; }
        public string Usuario { get; set; }
    }
}
