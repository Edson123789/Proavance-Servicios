using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ConsolidadoNotasDTO
    {
        public string NombreCurso { get; set; }
        public DateTime? InicioCurso {get;set; }
        public DateTime? FinCurso { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdCursoMoodle { get; set; }
        public decimal? Promedio { get; set; }
        public List<CronogramalistaCursosOonlineV2DTO> Autoevaluaciones { get; set; }
}
}
