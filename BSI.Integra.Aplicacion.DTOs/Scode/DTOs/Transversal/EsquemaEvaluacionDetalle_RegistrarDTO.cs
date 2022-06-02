using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaEvaluacionDetalle_RegistrarDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int Ponderacion { get; set; }
    }
    public class EsquemaEvaluacionDetalle_CongeladoDTO
    {
        public int Id { get; set; }
        public int IdEsquemaEvaluacion { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public string NombreEsquemaDetalle { get; set; }
        public int Ponderacion { get; set; }
    }
}
