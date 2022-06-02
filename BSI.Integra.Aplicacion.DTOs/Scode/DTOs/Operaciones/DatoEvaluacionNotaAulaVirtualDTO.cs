using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatoEvaluacionNotaAulaVirtualDTO
    {
        public int IdEvaluacion { get; set; }
        public string NombreEvaluacion { get; set; }
        public decimal? Nota { get; set; }
        public int? EscalaCalificacion { get; set; }
        public DateTime? Fecha { get; set; }
    }

    public class ListadoNotaAulaVirtualDTO
    {
        public string CodigoAlumno { get; set; }
        public int IdUsuarioMoodle { get; set; }
        public int IdCursoMoodle { get; set; }
        public string Curso { get; set; }
        public int IdEvaluacionMoodle { get; set; }
        public string Evaluacion { get; set; }
        public int? EscalaCalificacion { get; set; }
        public decimal? Nota { get; set; }
        public DateTime? Fecha { get; set; }
        public string TipoCurso { get; set; }
        public int IdSeccion { get; set; }
        public string Seccion { get; set; }
    }
}
