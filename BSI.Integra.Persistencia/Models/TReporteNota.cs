using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TReporteNota
    {
        public int Id { get; set; }
        public int? IdItem { get; set; }
        public int? IdAlumnoMoodle { get; set; }
        public int? IdCursoMoodle { get; set; }
        public string Nota { get; set; }
        public string NombreAutoevaluacion { get; set; }
        public string NombreCurso { get; set; }
        public string Fecha { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool? EstadoEvaluacion { get; set; }
        public int? NumeroSemana { get; set; }
        public int? EscalaCalificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
