using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CronogramaAutoEvaluacionV2DTO
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public string IdMatriculaCabecera { get; set; }
        public int IdCursoMoodle { get; set; }
        public int IdEvaluacionMoodle { get; set; }
        public string NombreCurso { get; set; }
        public string NombreEvaluacion { get; set; }
        public DateTime? FechaCronograma { get; set; }
        public DateTime? FechaRendicion { get; set; }
        public decimal? Nota { get; set; }
        public int Orden { get; set; }
        public int Version { get; set; }
    }

    public class CronogramalistaCursosOonlineV2DTO
    {
        public string NombreEvaluacion { get; set; }
        public DateTime? FechaCronograma { get; set; }
        public DateTime? FechaRendicion { get; set; }
        public decimal? Nota { get; set; }
        public int Orden { get; set; }
        public int IdCursoMoodle { get; set; }
        public int Version { get; set; }
        public decimal? Porcentaje { get; set; }
        public int? IdEvaluacionMoodle { get; set; }
    }

    public class CronogramalistaCursosOonlineV2PromedioDTO
    {
        public string CodigoMatricula { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdCursoMoodle { get; set; }
        public string NombreCurso { get; set; }
        public DateTime? FechaCronograma { get; set; }
        public DateTime? FechaRendicion { get; set; }
        public decimal? Promedio { get; set; }
    }
}
