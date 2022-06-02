using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaEvaluacion_DetalleCalificacionDTO
    {
        public int IdCriterioEvaluacion { get; set; }
        public string CriterioEvaluacion { get; set; }
        public decimal Valor { get; set; }
        public int Ponderacion { get; set; }
    }
}
