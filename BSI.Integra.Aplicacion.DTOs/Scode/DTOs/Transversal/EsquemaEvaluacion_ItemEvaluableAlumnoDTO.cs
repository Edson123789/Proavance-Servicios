using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class EsquemaEvaluacion_ItemEvaluableAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int Grupo { get; set; }

        public int IdCriterioEvaluacion { get; set; }
        public string CriterioEvaluacion { get; set; }

        public int IdEsquemaEvaluacionPGeneralDetalle { get; set; }
        public int IdEsquemaEvaluacion { get; set; }

        public int IdParametroEvaluacion { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }

        public decimal? ValorEscala { get; set; }

        public int? IdFormaCalificacionCriterio { get; set; }
        public int? IdFormaCalculoEvaluacion_Parametro { get; set; }
        public int? IdFormaCalculoEvaluacion_Criterio { get; set; }
        public int Ponderacion_Parametro { get; set; }
        public int IdFormaCalculoEvaluacion_Esquema { get; set; }
        public int Ponderacion_Criterio { get; set; }
    }
}
