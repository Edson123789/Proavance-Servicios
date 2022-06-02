using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMatriculaAlumnoMoodle
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdAlumno { get; set; }
        public int IdMatriculaMoodle { get; set; }
        public int IdUsuarioMoodle { get; set; }
        public int IdCursoMoodle { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
