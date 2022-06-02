using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CronogramaAutoEvaluacionDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdCurso { get; set; }
        public DateTime? FechaRendicion { get; set; }
        public int Nota { get; set; }
        public string NombreAutoEvaluacion { get; set; }
        public string NombreCurso { get; set; }
        public DateTime? FechaCronograma { get; set; }
    }
}
