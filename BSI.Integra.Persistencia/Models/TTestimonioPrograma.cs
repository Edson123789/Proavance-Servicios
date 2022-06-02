using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TTestimonioPrograma
    {
        public int Id { get; set; }
        public int? IdPgeneral { get; set; }
        public int CursoMoodleId { get; set; }
        public int UsuarioMoodleId { get; set; }
        public string Alumno { get; set; }
        public string Testimonio { get; set; }
        public string Pregunta { get; set; }
        public bool Autoriza { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
